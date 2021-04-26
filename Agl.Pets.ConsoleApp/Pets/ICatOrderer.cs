using Agl.Pets.ConsoleApp.Models;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using System.Collections.Generic;

namespace Agl.Pets.ConsoleApp.Pets
{
    public interface ICatOrderer
    {
        IList<CatGroupingModel> GetCatsGroupedByOwnerGender(IList<(Owner Owner, IList<Cat> Cats)> catOwners);
    }
}
