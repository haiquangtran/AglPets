using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agl.Pets.Infrastructure.PetOwners
{
    public interface IPetOwnerHttpClient
    {
        Task<IList<PetOwner>> GetPetOwners();
    }
}
