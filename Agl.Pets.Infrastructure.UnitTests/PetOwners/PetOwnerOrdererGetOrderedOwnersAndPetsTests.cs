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
    public class PetOwnerOrdererGetOrderedOwnersAndPetsTests
    {
        [Fact]
        public void GetOrderedOwnersAndPetsTest_ReturnsEmptyLists()
        {
            // Arrange

            List<PetOwner> petOwners = new();

            // Act

            PetOwnerOrderer.GetOrderedOwnersAndPets(AnimalTypes.Cat, petOwners, out List<Pet> malePets, out List<Pet> femalePets, out List<Pet> otherPets);

            // Assert

            Assert.Empty(malePets);
            Assert.Empty(femalePets);
            Assert.Empty(otherPets);
        }

        [Fact]
        public void GetOrderedOwnersAndPetsTest_GroupedByOwnerGender_ReturnsOrderedCats()
        {
            // Arrange

            List<PetOwner> petOwners = GetPetOwnerData();

            // Act

            PetOwnerOrderer.GetOrderedOwnersAndPets(AnimalTypes.Cat, petOwners, out List<Pet> malePets, out List<Pet> femalePets, out List<Pet> otherPets);

            // Assert

            Assert.True(
                malePets.All(x => x.Type == AnimalTypes.Cat)
                && femalePets.All(x => x.Type == AnimalTypes.Cat)
                && otherPets.All(x => x.Type == AnimalTypes.Cat));

            Assert.Equal("a", malePets[0].Name);
            Assert.Equal("b", malePets[1].Name);
            Assert.Equal("c", malePets[2].Name);

            Assert.Equal("a", femalePets[0].Name);
            Assert.Equal("b", femalePets[1].Name);

            Assert.Equal("a", otherPets[0].Name);
        }

        private static List<PetOwner> GetPetOwnerData()
        {
            return new List<PetOwner>
            {
                new PetOwner {
                    Gender = GenderTypes.Male,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "b",
                            Type = AnimalTypes.Cat
                        },
                        new Pet
                        {
                            Name = "a",
                            Type = AnimalTypes.Cat
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
                            Type = AnimalTypes.Cat
                        }
                    },
                },
                new PetOwner {
                    Gender = GenderTypes.Female,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "b",
                            Type = AnimalTypes.Cat
                        },
                        new Pet
                        {
                            Name = "a",
                            Type = AnimalTypes.Cat
                        },
                        new Pet
                        {
                            Name = "dog",
                            Type = "dog"
                        },
                    },
                },
                new PetOwner {
                    Gender = "asdfasdf",
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "b",
                            Type = AnimalTypes.Cat
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
