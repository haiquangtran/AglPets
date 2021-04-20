using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Infrastructure.PetOwners
{
    [ExcludeFromCodeCoverage]
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}