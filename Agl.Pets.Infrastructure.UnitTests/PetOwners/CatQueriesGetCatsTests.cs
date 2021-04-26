using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using Agl.Pets.Infrastructure.Pets;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Pet = Agl.Pets.Infrastructure.PetOwners.Pet;

namespace Agl.Pets.Infrastructure.UnitTests
{
    public class CatQueriesGetCatsTests
        : Tests
    {
        [Fact]
        public async void GetCatsTest_ReturnsEmptyLists()
        {
            // Arrange

            List<PetOwner> petOwners = new();
            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            mockPetOwnerHttpClient.GetPetOwners().Returns(petOwners);

            var queries = CreateQueries(mockPetOwnerHttpClient);

            // Act

            var test = await queries.GetCats();

            // Assert

            Assert.Empty(test);
        }

        [Fact]
        public async void GetCatsTest()
        {
            // Arrange
            var petOwners = new List<PetOwner>
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
                    Gender = GenderTypes.Female,
                    Pets = new Pet[] {
                        new Pet
                        {
                            Name = "c",
                            Type = AnimalTypes.Cat
                        }
                    },
                }
            };

            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            mockPetOwnerHttpClient.GetPetOwners().Returns(petOwners);

            var queries = CreateQueries(mockPetOwnerHttpClient);

            // Act

            var test = await queries.GetCats();

            // Assert
            Assert.Equal(petOwners[0].Gender, test[0].Owner.Gender);
            Assert.Equal(
                petOwners[0].Pets.Where(x => x.Type == AnimalTypes.Cat).Select(x => x.Name)
                , 
                test[0].Cats.Select(x => x.Name)
            );

            Assert.Equal(petOwners[1].Gender, test[1].Owner.Gender);
            Assert.Equal(
                petOwners[1].Pets.Where(x => x.Type == AnimalTypes.Cat).Select(x => x.Name)
                ,
                test[1].Cats.Select(x => x.Name)
            );
        }

        private static ICatQueries CreateQueries(IPetOwnerHttpClient petOwnerHttpClient)
        {
            var mapper = CreateMapper();

            return new CatQueries(petOwnerHttpClient, mapper);
        }
    }
}
