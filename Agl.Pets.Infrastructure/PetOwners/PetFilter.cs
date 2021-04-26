using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Infrastructure.PetOwners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agl.Pets.ConsoleApp.PetOwners
{
    public static class PetFilter
    {
        /// <summary>
        /// 
        ///  List all the pets of a specific animal type in alphabetical order by the gender of their owners.
        /// 
        /// </summary>
        /// <param name="animalType">Specific type of pet to be filtered by</param>
        /// <param name="petOwners">A list of all the pet owners to be filtered by</param>
        /// <param name="maleOwners">A list containing pet names of the filtered pet that are owned by male owners</param>
        /// <param name="femaleOwners">A list containing pet names of the filtered pet that are owned by female owners</param>
        /// <param name="otherOwners">A list containing pet names of the filtered pet that are owned by "other" gender owners</param>
        public static void FilterPetsByType(string animalType, IList<PetOwner> petOwners, out List<Pet> maleOwners, out List<Pet> femaleOwners, out List<Pet> otherOwners)
        {
            FilterPetByAnimalType(animalType, petOwners, out maleOwners, out femaleOwners, out otherOwners);

            OrderPetsByName(ref maleOwners, ref femaleOwners, ref otherOwners);
        }

        private static void FilterPetByAnimalType(string animalType, IList<PetOwner> petOwners, out List<Pet> maleOwners, out List<Pet> femaleOwners, out List<Pet> otherOwners)
        {
            maleOwners = new List<Pet>();
            femaleOwners = new List<Pet>();
            otherOwners = new List<Pet>();

            foreach (var petOwner in petOwners)
            {
                // Filter by type of pet

                var pets = petOwner.Pets != null
                    ? petOwner.Pets
                        .Where(pet => pet.Type.Equals(animalType, StringComparison.InvariantCultureIgnoreCase))
                        .ToList()
                    : new List<Pet>();

                // Group by gender

                if (pets.Any())
                {
                    if (petOwner.Gender.Equals(GenderTypes.Male, StringComparison.InvariantCultureIgnoreCase))
                        maleOwners.AddRange(pets);
                    else if (petOwner.Gender.Equals(GenderTypes.Female, StringComparison.InvariantCultureIgnoreCase))
                        femaleOwners.AddRange(pets);
                    else
                        otherOwners.AddRange(pets);
                }
            }
        }

        private static void OrderPetsByName(ref List<Pet> maleOwners, ref List<Pet> femaleOwners, ref List<Pet> otherOwners)
        {
            // Sort alphabetically by the pet's name

            maleOwners = maleOwners.OrderBy(p => p.Name).ToList();
            femaleOwners = femaleOwners.OrderBy(p => p.Name).ToList();
            otherOwners = otherOwners.OrderBy(p => p.Name).ToList();
        }
    }
}
