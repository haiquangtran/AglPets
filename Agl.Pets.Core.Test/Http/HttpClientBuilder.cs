using System;
using System.Net.Http;

namespace Agl.Pets.Core.Test.Http
{
    public interface IHttpClientBuilder
    {
        IHttpClientBuilder WithPath(string path);
        IHttpClientBuilder WithContent(string content);
        HttpClient Build();
    }

    public class HttpClientBuilder
         : IHttpClientBuilder
    {
        private readonly MockHttpMessageHandlerOptions _options;

        public HttpClientBuilder()
        {
            _options = new MockHttpMessageHandlerOptions();
        }

        public IHttpClientBuilder WithPath(string path)
        {
            _options.Path = path;
            return this;
        }

        public IHttpClientBuilder WithContent(string content)
        {
            _options.Content = content;
            return this;
        }

        public HttpClient Build()
        {
            var handler = (HttpMessageHandler)new MockOptionsHttpMessageHandler(_options);

            if (_options.Handler != null)
            {
                _options.Handler.InnerHandler = handler;
                handler = _options.Handler;
            }

            return new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };
        }
    }
}
