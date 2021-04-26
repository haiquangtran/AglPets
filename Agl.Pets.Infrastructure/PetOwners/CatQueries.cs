using Agl.Pets.Domain.Pets;
using Agl.Pets.Domain.PetOwners;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Agl.Pets.Infrastructure.PetOwners;
using Pet = Agl.Pets.Infrastructure.PetOwners.Pet;

namespace Agl.Pets.Infrastructure.Pets
{
    public class CatQueries
        : ICatQueries
    {
        private readonly IMapper _mapper;
        private readonly IPetOwnerHttpClient _petOwnerClient;

        public CatQueries(IPetOwnerHttpClient petOwnerClient, IMapper mapper)
        {
            _petOwnerClient = petOwnerClient;
            _mapper = mapper;
        }

        public async Task<IList<(Owner Owner, IList<Cat> Cats)>> GetCats()
        {
            var petOwners = await _petOwnerClient.GetPetOwners();

            return FilterCatOwners(petOwners);
        }

        private IList<(Owner owner, IList<Cat> Cats)> FilterCatOwners(IList<PetOwner> petOwners)
        {
            var catOwners = new List<(Owner owner, IList<Cat> Cats)>();

            foreach (var owner in petOwners)
            {
                // Filter by cats

                var cats = owner.Pets != null 
                    ? owner.Pets.Where(p => p.Type == AnimalTypes.Cat).ToList()
                    : new List<Pet>();

                // Pet owner has cats

                if (cats.Any())
                    catOwners.Add((_mapper.Map<Owner>(owner), _mapper.Map<IList<Cat>>(cats)));
            }

            return catOwners;
        }
    }
}