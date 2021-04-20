using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Api
{
    public interface IJsonApiClient
    {
        Task<TResponse> GetAsync<TResponse>(string relativeUrl);
    }
}
