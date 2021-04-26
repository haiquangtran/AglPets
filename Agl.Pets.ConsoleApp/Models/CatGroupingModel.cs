using Agl.Pets.Domain.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.ConsoleApp.Models
{
    public class CatGroupingModel
    {
        public string OwnerGender { get; set; }
        public IList<Cat> Cats { get; set; }
    }
}
