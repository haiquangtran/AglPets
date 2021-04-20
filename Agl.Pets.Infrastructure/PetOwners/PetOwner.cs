using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Infrastructure.PetOwners
{
    [ExcludeFromCodeCoverage]
    public class PetOwner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public Pet[] Pets { get; set; }
    }
}