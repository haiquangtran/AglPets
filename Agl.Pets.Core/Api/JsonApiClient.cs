using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Api
{
    [ExcludeFromCodeCoverage]
    public class JsonApiClient
        : IJsonApiClient
    {
        private const string MediaType = "application/json";
        private readonly HttpClient _httpClient;

        public JsonApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
        }

        public async Task<TResponse> GetAsync<TResponse>(string relativeUrl)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            await EnsureSuccess(relativeUrl, httpResponse);
            return await GetResponse<TResponse>(httpResponse);
        }

        private static async Task<TResponse> GetResponse<TResponse>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }

        private async Task EnsureSuccess(string relativeUrl, HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return;

            if (response.StatusCode == HttpStatusCode.NoContent)
                return;

            throw new ApiException(_httpClient.BaseAddress.ToString(), relativeUrl, response.StatusCode, await response.Content.ReadAsStringAsync(), response.Content?.Headers?.ContentType?.ToString());
        }
    }
}
