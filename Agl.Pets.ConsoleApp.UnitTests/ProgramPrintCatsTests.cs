using Agl.Pets.ConsoleApp.Models;
using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Core.Test.Api;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using Agl.Pets.Infrastructure.Pets;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Agl.Pets.ConsoleApp.UnitTests
{
    public class ProgramPrintCatsTests
    {
        [Fact]
        public async void PrintCatsTest_ShouldCallFormatPetNamesForEachGenderGrouping()
        {
            // Arrange
      
            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetPrinter.FormatPetNames(Arg.Any<string>(), Arg.Any<IEnumerable<Domain.Pets.Pet>>()).Returns("test");
            var mockCatQueries = Substitute.For<ICatQueries>();
            var mockCatOrderer = Substitute.For<ICatOrderer>();
            mockCatOrderer.GetCatsGroupedByOwnerGender(Arg.Any<IList<(Owner Owner, IList<Cat> Cats)>>())
                .Returns(new List<CatGroupingModel>()
                {
                    new CatGroupingModel
                    {
                        Cats = new List<Cat>(),
                        OwnerGender = GenderTypes.Male
                    },
                    new CatGroupingModel
                    {
                        Cats = new List<Cat>(),
                        OwnerGender = GenderTypes.Female
                    }
                });

            mockServiceProvider.GetService<ICatQueries>().Returns(mockCatQueries);
            mockServiceProvider.GetService<ICatOrderer>().Returns(mockCatOrderer);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Program.PrintCats(mockServiceProvider);

            // Assert
            mockPetPrinter.Received(2).FormatPetNames(Arg.Is<string>(x => x == GenderTypes.Male
            || x == GenderTypes.Female), Arg.Any<IEnumerable<Domain.Pets.Pet>>());
        }

        [Fact]
        public async void PrintCatsTest_ShouldCallCatQueriesGetCats()
        {
            // Arrange

            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetPrinter.FormatPetNames(Arg.Any<string>(), Arg.Any<IEnumerable<Domain.Pets.Pet>>()).Returns("test");

            var mockCatQueries = Substitute.For<ICatQueries>();
            var mockCatOrderer = Substitute.For<ICatOrderer>();
            mockCatOrderer.GetCatsGroupedByOwnerGender(Arg.Any<IList<(Owner Owner, IList<Cat> Cats)>>())
                .Returns(new List<CatGroupingModel>());

            mockServiceProvider.GetService<ICatQueries>().Returns(mockCatQueries);
            mockServiceProvider.GetService<ICatOrderer>().Returns(mockCatOrderer);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Program.PrintCats(mockServiceProvider);

            // Assert
            await mockCatQueries.Received().GetCats();
        }

        [Fact]
        public async void PrintCatsTest_ShouldCallCatOrdererGetCatsGroupedByOwnerGender()
        {
            // Arrange

            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetPrinter.FormatPetNames(Arg.Any<string>(), Arg.Any<IEnumerable<Domain.Pets.Pet>>()).Returns("test");

            var mockCatQueries = Substitute.For<ICatQueries>();
            var mockCatOrderer = Substitute.For<ICatOrderer>();
            mockCatOrderer.GetCatsGroupedByOwnerGender(Arg.Any<IList<(Owner Owner, IList<Cat> Cats)>>())
                .Returns(new List<CatGroupingModel>());

            mockServiceProvider.GetService<ICatQueries>().Returns(mockCatQueries);
            mockServiceProvider.GetService<ICatOrderer>().Returns(mockCatOrderer);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Program.PrintCats(mockServiceProvider);

            // Assert
            mockCatOrderer.Received().GetCatsGroupedByOwnerGender(Arg.Any<IList<(Owner Owner, IList<Cat> Cats)>>());
        }

        [Fact]
        public async void PrintCatsTest_ThrowsException()
        {
            // Arrange
            var mockServiceProvider = Substitute.For<IServiceProvider>();
            var mockPetOwnerHttpClient = Substitute.For<IPetOwnerHttpClient>();
            var mockCatQueries = Substitute.For<ICatQueries>();
            mockCatQueries.GetCats().Throws(new InvalidOperationException());
            
            var mockPetPrinter = Substitute.For<IPetPrinter>();
            mockPetPrinter.FormatPetNames(Arg.Any<string>(), Arg.Any<IEnumerable<Domain.Pets.Pet>>()).Returns("test");

            mockServiceProvider.GetService<ICatQueries>().Returns(mockCatQueries);
            mockServiceProvider.GetService<IPetPrinter>().Returns(mockPetPrinter);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() => Program.PrintCats(mockServiceProvider));
        }
    }
}
