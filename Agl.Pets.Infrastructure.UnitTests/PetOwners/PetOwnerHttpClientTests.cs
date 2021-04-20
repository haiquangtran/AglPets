using Agl.Pets.ConsoleApp.PetOwners;
using Agl.Pets.Core.Test.Api;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Agl.Pets.Infrastructure.UnitTests
{
    public class PetOwnerHttpClientTests
        : ApiClientTests
    {
        [Fact]
        public async Task GetPetOwnersTest()
        {
            // Arrange
            var petName = "Spikey";
            var ownerName = "Johnny";
            var gender = "Male";

            var apiClient = CreateApiClientWithPath($"people.json", $"[ {{ Name: \"{ownerName}\", Gender: \"{gender}\", Pets: [ {{ Name: \"{petName}\" }} ] }}]");

            // Act
            var response = await apiClient.GetPetOwners();

            // Assert
            Assert.Equal(1, response.Count);
            Assert.Equal(ownerName, response[0].Name);
            Assert.Equal(gender, response[0].Gender);

            Assert.Single(response[0].Pets);
            Assert.Equal(petName, response[0].Pets[0].Name);
        }

        private static IPetOwnerHttpClient CreateApiClientWithPath(string path, string content)
        {
            var httpClient = CreateHttpClientBuilder()
                .WithPath(path)
                .WithContent(content)
                .Build();
            return new PetOwnerHttpClient(httpClient);
        }
    }
}

