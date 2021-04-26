using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using System.Collections.Generic;
using Xunit;

namespace Agl.Pets.ConsoleApp.UnitTests.Pets
{
    public class PetPrinterPrintPetNamesByPetTypeTests
    {
        [Fact]
        public void PrintPetNamesByPetTypeTest_ShouldCallPrintPetNamesByPetType()
        {
            // Arrange
            var printer = CreatePetPrinter();

            // Act
            var test = printer.PrintPetNamesByPetType(AnimalTypes.Cat, new List<PetOwner>());

            // Assert
            Assert.Contains("Male", test);
            Assert.Contains("Female", test);
            Assert.Contains("Other", test);
        }

        [Fact]
        public void PrintPetNamesByPetTypeTest_ReturnsCatNamesOnly()
        {
            var petOwners = new List<PetOwner>()
            {
                new PetOwner
                {
                    Gender = GenderTypes.Male,
                    Pets = new [] {
                        new Pet {
                            Name = "cat-a",
                            Type = AnimalTypes.Cat
                        },
                        new Pet {
                            Name = "cat-b",
                            Type = AnimalTypes.Cat
                        },
                        new Pet {
                            Name = "crocodile",
                            Type = "crocodile"
                        }
                    }
                }
            };
            var printer = CreatePetPrinter();

            // Act
            var test = printer.PrintPetNamesByPetType(AnimalTypes.Cat, petOwners);

            // Assert
            Assert.Contains("cat-a", test);
            Assert.Contains("cat-b", test);
            Assert.Contains("cat-b", test);
            Assert.DoesNotContain("crocodile", test);
        }

        public static IPetPrinter CreatePetPrinter()
        {
            return new PetPrinter();
        }
    }
}
