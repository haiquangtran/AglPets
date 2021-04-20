using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Api
{
    [ExcludeFromCodeCoverage]
    public class ApiOptions
    {
        public string BaseUrl { get; set; } = null!;
        public int? TimeoutSecs { get; set; }
    }
}
