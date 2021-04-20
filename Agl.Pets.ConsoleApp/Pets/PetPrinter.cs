using Agl.Pets.ConsoleApp.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Agl.Pets.ConsoleApp.Pets
{
    [ExcludeFromCodeCoverage]
    public static class PetPrinter
    {
        public static string GetFormattedOwnersAndPetsText(string animalType, IList<PetOwner> petOwners)
        {
            PetOwnerOrderer.GetOrderedOwnersAndPets(animalType, petOwners, out List<Pet> maleOwnerOfCats, out List<Pet> femaleOwnerOfCats, out List<Pet> otherOwnerOfCats);

            // Print results

            var result = new StringBuilder();

            PetPrinter.FormatPetPrint("Male", maleOwnerOfCats, result);
            PetPrinter.FormatPetPrint("Female", femaleOwnerOfCats, result);
            PetPrinter.FormatPetPrint("Other", otherOwnerOfCats, result);

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
