using Agl.Pets.Infrastructure.Mapping;
using AutoMapper;

namespace Agl.Pets.Infrastructure.UnitTests
{
    public abstract class Tests
    {
        protected static IMapper CreateMapper()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PetMappingProfile());

            }).CreateMapper();
        }
    }
}
