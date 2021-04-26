using Agl.Pets.Infrastructure.PetOwners;
using System.Collections.Generic;

namespace Agl.Pets.ConsoleApp.Pets
{
    public interface IPetPrinter
    {
        public string PrintPetNamesByPetType(string animalType, IList<PetOwner> petOwners);
    }
}
