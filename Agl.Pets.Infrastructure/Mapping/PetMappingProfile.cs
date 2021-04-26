using Agl.Pets.Domain.PetOwners;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pet = Agl.Pets.Infrastructure.PetOwners.Pet;

namespace Agl.Pets.Infrastructure.Mapping
{
    public class PetMappingProfile
        : Profile
    {
        public PetMappingProfile()
        {
            CreateMap<PetOwner, Owner>()
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender));

            CreateMap<Pet, Cat>()
              .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
        }
    }
}
