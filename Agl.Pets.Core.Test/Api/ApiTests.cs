using Agl.Pets.Core.Test.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHttpClientBuilder = Agl.Pets.Core.Test.Http.IHttpClientBuilder;

namespace Agl.Pets.Core.Test.Api
{
    public abstract class ApiClientTests
    {
        protected static IHttpClientBuilder CreateHttpClientBuilder()
        {
            return new HttpClientBuilder();
        }
    }

}
