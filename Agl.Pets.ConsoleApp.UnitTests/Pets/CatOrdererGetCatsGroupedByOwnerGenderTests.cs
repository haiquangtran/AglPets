using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using System.Collections.Generic;
using Xunit;
using Pet = Agl.Pets.Domain.Pets.Pet;

namespace Agl.Pets.ConsoleApp.UnitTests.Pets
{
    public class CatOrdererGetCatsGroupedByOwnerGenderTests
    {
        [Fact]
        public void GetCatsGroupedByOwnerGenderTest_ReturnsOrderedCatNamesAlphabeticallyAndGroupedByOwnerGender()
        {
            // Arrange
            var otherGender = "other";
            var catOwners = new List<(Owner Owner, IList<Cat> Cats)>
            {
                (
                    new Owner
                    {
                        Gender = GenderTypes.Male
                    },
                    new List<Cat> 
                    {
                        new Cat { Name = "b" }, new Cat { Name = "c" }, new Cat { Name = "a" },
                    }
                ),
                (
                    new Owner
                    {
                        Gender = GenderTypes.Male
                    },
                    new List<Cat>
                    { 
                        new Cat { Name = "d" } 
                    }
                ),
                (
                    new Owner
                    {
                        Gender = GenderTypes.Female
                    },
                    new List<Cat>
                    {
                        new Cat
                        {
                            Name = "a"
                        },
                        new Cat
                        {
                            Name = "b"
                        }
                    }
                ),
                (
                    new Owner
                    {
                        Gender = otherGender
                    },
                    new List<Cat>
                    {
                        new Cat
                        {
                            Name = "a"
                        }
                    }
                )
            };
            var catOrderer = CreateOrderer();

            // Act
            var test = catOrderer.GetCatsGroupedByOwnerGender(catOwners);

            // Assert
            Assert.Equal(GenderTypes.Male, test[0].OwnerGender);
            Assert.Equal("a", test[0].Cats[0].Name);
            Assert.Equal("b", test[0].Cats[1].Name);
            Assert.Equal("c", test[0].Cats[2].Name);
            Assert.Equal("d", test[0].Cats[3].Name);

            Assert.Equal(GenderTypes.Female, test[1].OwnerGender);
            Assert.Equal("a", test[1].Cats[0].Name);
            Assert.Equal("b", test[1].Cats[1].Name);

            Assert.Equal(otherGender, test[2].OwnerGender);
            Assert.Equal("a", test[2].Cats[0].Name);
        }

        [Fact]
        public void GetCatsGroupedByOwnerGenderTest_ReturnsEmptyList()
        {
            // Arrange
            var catOwners = new List<(Owner Owner, IList<Cat> Cats)>{};
            var catOrderer = CreateOrderer();

            // Act
            var test = catOrderer.GetCatsGroupedByOwnerGender(catOwners);

            // Assert
            Assert.Empty(test);
        }

        private static ICatOrderer CreateOrderer()
        {
            return new CatOrderer();
        }
    }
}
