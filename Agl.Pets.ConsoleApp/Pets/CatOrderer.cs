using Agl.Pets.ConsoleApp.Models;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using System.Collections.Generic;
using System.Linq;

namespace Agl.Pets.ConsoleApp.Pets
{
    public class CatOrderer
        : ICatOrderer
    {
        public IList<CatGroupingModel> GetCatsGroupedByOwnerGender(IList<(Owner Owner, IList<Cat> Cats)> catOwners)
        {
            // Group cats by owner gender

            var groupedCats = catOwners
                .GroupBy(o => o.Owner.Gender);

            // Order by cat name

            return groupedCats.Select(group => new CatGroupingModel
            {
                OwnerGender = group.Key,
                Cats = group
                        .SelectMany(g => g.Cats)
                        .OrderBy(c => c.Name)
                        .ToList()
            })
            .ToList();
        }
    }
}
