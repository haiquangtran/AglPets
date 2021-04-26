using Agl.Pets.Domain.Pets;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agl.Pets.ConsoleApp.Pets
{
    public class PetPrinter
        : IPetPrinter
    {
        public string FormatPetNames(string genderGroup, IEnumerable<Pet> pets)
        {
            StringBuilder result = new();

            result.AppendLine();
            result.AppendLine(genderGroup);

            if (pets.Any())
            {
                result.Append('\t');
                result.AppendJoin("\n\t", pets.Select(x => x.Name));
            }
            else
            {
                result.AppendLine("\tThere are no pets.");
            }
            result.AppendLine();

            return result.ToString();
        }
    }
}
