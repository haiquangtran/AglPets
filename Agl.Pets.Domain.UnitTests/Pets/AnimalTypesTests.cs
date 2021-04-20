using Agl.Pets.Domain.Pets;
using Xunit;

namespace Agl.Pets.Domain.UnitTests.Pets
{
    public class AnimalTypesTests
    {
        [Fact]
        public void AnimalTypesTest_ReturnsCat()
        {
            Assert.Equal("Cat", AnimalTypes.Cat);
        }
    }
}
