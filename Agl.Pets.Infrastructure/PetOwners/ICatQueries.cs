using Agl.Pets.Domain.Pets;
using Agl.Pets.Domain.PetOwners;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agl.Pets.Infrastructure.Pets
{
    public interface ICatQueries
    {
        Task<IList<(Owner Owner, IList<Cat> Cats)>> GetCats();
    }
}