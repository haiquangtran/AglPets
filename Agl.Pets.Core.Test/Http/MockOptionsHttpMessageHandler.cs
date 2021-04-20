using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Test.Http
{
    public class MockOptionsHttpMessageHandler
          : HttpMessageHandler
    {
        private readonly MockHttpMessageHandlerOptions _options;

        public MockOptionsHttpMessageHandler(MockHttpMessageHandlerOptions options)
        {
            _options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var path = _options.Path != null && !_options.Path.StartsWith("/")
                ? "/" + _options.Path
                : _options.Path;

            if (request.RequestUri.AbsolutePath == path)
                return new HttpResponseMessage
                {
                    StatusCode = _options.StatusCode,
                    Content = _options.Content == null ? null : new StringContent(_options.Content)
                };

            throw new InvalidOperationException("No path has been set.");
        }
    }
}
