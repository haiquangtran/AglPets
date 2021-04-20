using Agl.Pets.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Infrastructure.PetOwners
{
    public class PetOwnerHttpClient
        : JsonApiClient, IPetOwnerHttpClient
    {
        public PetOwnerHttpClient(HttpClient httpClient)
          : base(httpClient)
        {
        }

        public async Task<IList<PetOwner>> GetPetOwners()
        {
            return await GetAsync<IList<PetOwner>>("/people.json");
        }
    }
}
