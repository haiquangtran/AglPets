using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using System.Collections.Generic;
using Xunit;
using Pet = Agl.Pets.Domain.Pets.Pet;

namespace Agl.Pets.ConsoleApp.UnitTests.Pets
{
    public class PetPrinterFormatPetNamesTests
    {
        [Fact]
        public void FormatPetNamesTest_NoPets_ReturnsNoPetsMessage()
        {
            // Arrange
            var printer = CreatePetPrinter();

            // Act
            var test = printer.FormatPetNames(GenderTypes.Male, new List<Pet>());

            // Assert
            Assert.Contains("\tThere are no pets.", test);
        }

        [Fact]
        public void FormatPetNamesTest_ReturnsHasGenderHeading()
        {
            // Arrange
            var printer = CreatePetPrinter();

            // Act
            var test = printer.FormatPetNames(GenderTypes.Male, new List<Pet>());

            // Assert
            Assert.Contains(GenderTypes.Male, test);
        }

        [Fact]
        public void FormatPetNamesTest_ReturnsPetNames()
        {
            // Arrange
            var pets = new List<Pet>()
            {
                new Cat
                {
                    Name = "cat-a"
                },
                new Cat
                {
                    Name = "cat-b"
                }
            };
            var printer = CreatePetPrinter();

            // Act
            var test = printer.FormatPetNames(GenderTypes.Male, pets);

            // Assert
            Assert.Contains("cat-a", test);
            Assert.Contains("cat-b", test);
        }

        public static IPetPrinter CreatePetPrinter()
        {
            return new PetPrinter();
        }
    }
}
