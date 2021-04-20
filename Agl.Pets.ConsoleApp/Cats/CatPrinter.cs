using Agl.Pets.ConsoleApp.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Agl.Pets.ConsoleApp.Cats
{
    [ExcludeFromCodeCoverage]
    public static class CatPrinter
    {
        public static void PrintOwnersAndCats(IList<PetOwner> petOwners)
        {
            PetOwnerOrderer.GetOrderedOwnersAndPets(AnimalTypes.Cat, petOwners, out List<Pet> maleOwnerOfCats, out List<Pet> femaleOwnerOfCats, out List<Pet> otherOwnerOfCats);

            // Print results

            var result = new StringBuilder();

            CatPrinter.FormatCatPrint("Male", maleOwnerOfCats, result);
            CatPrinter.FormatCatPrint("Female", femaleOwnerOfCats, result);
            CatPrinter.FormatCatPrint("Other", otherOwnerOfCats, result);

            // Write to console

            Console.WriteLine(result);
        }

        private static void FormatCatPrint(string gender, List<Pet> cats, StringBuilder result)
        {
            result.AppendLine();
            result.AppendLine(gender);

            if (cats.Any())
            {
                result.Append("\t");
                result.AppendJoin("\n\t", cats.Select(x => x.Name));
            }
            else
            {
                result.AppendLine("\tThere are no cat owners.");
            }

            result.AppendLine();
        }
    }
}
