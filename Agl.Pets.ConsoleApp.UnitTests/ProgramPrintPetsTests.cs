using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Core.Test.Api;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Agl.Pets.ConsoleApp.UnitTests
{
    public class ProgramPrintPetsTests
    {
        [Fact]
        public async void PrintPetsTest_ShouldCallPetPrinterGetFormattedOwnersAndPetsText()
        {
            // Arrange
            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            mockPetOwnerHttpClient.GetPetOwners().Returns(new List<PetOwner>());

            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetPrinter.GetFormattedOwnersAndPetsText(Arg.Any<string>(), Arg.Any<List<PetOwner>>()).Returns("test");

            mockServiceProvider.GetService<IPetOwnerHttpClient>().Returns(mockPetOwnerHttpClient);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Program.PrintPets(AnimalTypes.Cat, mockServiceProvider);

            // Assert
            mockPetPrinter.Received().GetFormattedOwnersAndPetsText(AnimalTypes.Cat, Arg.Any<List<PetOwner>>());
        }

        [Fact]
        public async void PrintPetsTest_ShouldCallGetPetOwnersApi()
        {
            // Arrange
            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            var mockPetPrinter = Substitute.For<IPetPrinter>();

            mockServiceProvider.GetService<IPetOwnerHttpClient>().Returns(mockPetOwnerHttpClient);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Program.PrintPets(AnimalTypes.Cat, mockServiceProvider);

            // Assert
            await mockPetOwnerHttpClient.Received().GetPetOwners();
        }

        [Fact]
        public async void PrintPetsTest_ThrowsException()
        {
            // Arrange
            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetOwnerHttpClient.GetPetOwners().Throws(new InvalidOperationException());

            mockServiceProvider.GetService<IPetOwnerHttpClient>().Returns(mockPetOwnerHttpClient);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() => Program.PrintPets(AnimalTypes.Cat, mockServiceProvider));
        }
    }
}
