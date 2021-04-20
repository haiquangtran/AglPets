using Agl.Pets.ConsoleApp.PetOwners;
using Agl.Pets.Infrastructure.PetOwners;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agl.Pets.ConsoleApp.Pets
{
    public class PetPrinter
        : IPetPrinter
    {
        public string GetFormattedOwnersAndPetsText(string animalType, IList<PetOwner> petOwners)
        {
            PetOwnerOrderer.GetOrderedOwnersAndPets(animalType, petOwners, out List<Pet> maleOwnerOfCats, out List<Pet> femaleOwnerOfCats, out List<Pet> otherOwnerOfCats);

            // Print results

            var result = new StringBuilder();

            result.AppendLine();

            FormatPetPrint("Male", maleOwnerOfCats, result);
            FormatPetPrint("Female", femaleOwnerOfCats, result);
            FormatPetPrint("Other", otherOwnerOfCats, result);

            result.AppendLine();

            // Return formatted result

            return result.ToString();
        }

        private static void FormatPetPrint(string genderGroup, List<Pet> pets, StringBuilder result)
        {
            result.AppendLine();
            result.AppendLine(genderGroup);

            if (pets.Any())
            {
                result.Append("\t");
                result.AppendJoin("\n\t", pets.Select(x => x.Name));
            }
            else
            {
                result.AppendLine("\tThere are no pets.");
            }

            result.AppendLine();
        }
    }
}
