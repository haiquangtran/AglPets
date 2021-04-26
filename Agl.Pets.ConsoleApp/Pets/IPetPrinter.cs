using Agl.Pets.Domain.Pets;
using System.Collections.Generic;

namespace Agl.Pets.ConsoleApp.Pets
{
    public interface IPetPrinter
    {
        public string FormatPetNames(string genderGroup, IEnumerable<Pet> pets);
    }
}
