using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Test.Http
{
    public class MockHttpMessageHandlerOptions
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Content { get; set; }
        public string Path { get; set; }
        public DelegatingHandler Handler { get; set; }
    }
}
