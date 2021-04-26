using Agl.Pets.ConsoleApp.PetOwners;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Agl.Pets.Infrastructure.UnitTests
{
    public class PetFilterFilterPetsByTypeTests
    {
        [Fact]
        public void FilterPetsByTypeTest_ReturnsEmptyLists()
        {
            // Arrange

            List<PetOwner> petOwners = new();

            // Act

            PetFilter.FilterPetsByType(AnimalTypes.Cat, petOwners, out List<Pet> malePets, out List<Pet> femalePets, out List<Pet> otherPets);

            // Assert

            Assert.Empty(malePets);
            Assert.Empty(femalePets);
            Assert.Empty(otherPets);
        }

        [Theory]
        [InlineData(AnimalTypes.Cat)]
        [InlineData(AnimalTypes.Dog)]
        [InlineData(AnimalTypes.Fish)]
        public void FilterPetsByTypeTest_GroupedByOwnerGender_ReturnsOrderedCats(string animalType)
        {
            // Arrange

            List<PetOwner> petOwners = GetPetOwnerData(animalType);

            // Act

            PetFilter.FilterPetsByType(animalType, petOwners, out List<Pet> malePets, out List<Pet> femalePets, out List<Pet> otherPets);

            // Assert

            Assert.True(
                malePets.All(x => x.Type == animalType)
                && femalePets.All(x => x.Type == animalType)
                && otherPets.All(x => x.Type == animalType));

            Assert.Equal("a", malePets[0].Name);
            Assert.Equal("b", malePets[1].Name);
            Assert.Equal("c", malePets[2].Name);

            Assert.Equal("a", femalePets[0].Name);
            Assert.Equal("b", femalePets[1].Name);

            Assert.Equal("other", otherPets[0].Name);
        }

        private static List<PetOwner> GetPetOwnerData(string animalType = AnimalTypes.Cat)
        {
            return new List<PetOwner>
            {
                new PetOwner {
                    Gender = GenderTypes.Male,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "b",
                            Type = animalType
                        },
                        new Pet
                        {
                            Name = "a",
                            Type = animalType
                        },
                        new Pet
                        {
                            Name = "chicken",
                            Type = "chicken"
                        },
                    },
                },
                new PetOwner {
                    Gender = GenderTypes.Male,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "c",
                            Type = animalType
                        }
                    },
                },
                new PetOwner {
                    Gender = GenderTypes.Female,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "b",
                            Type = animalType
                        },
                        new Pet
                        {
                            Name = "a",
                            Type = animalType
                        },
                        new Pet
                        {
                            Name = "chicken",
                            Type = "chicken"
                        },
                    },
                },
                new PetOwner {
                    Gender = "asdfasdf",
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "other",
                            Type = animalType
                        },
                        new Pet
                        {
                            Name = "duck",
                            Type = "Duck"
                        },
                    },
                },
            };
        }
    }
}
