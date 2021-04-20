using Agl.Pets.Domain.PetOwners;
using Xunit;

namespace Agl.Pets.Domain.UnitTests.PetOwners
{
    public class GenderTypesTests
    {
        [Fact]
        public void GenderTypesTest_ReturnsMale()
        {
            Assert.Equal("Male", GenderTypes.Male);
        }

        [Fact]
        public void GenderTypesTest_ReturnsFemale()
        {
            Assert.Equal("Female", GenderTypes.Female);
        }
    }
}
