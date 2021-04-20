using Agl.Pets.Core.Api;
using Agl.Pets.Infrastructure.PetOwners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.ConsoleApp.Plumbing
{
    [ExcludeFromCodeCoverage]
    public static class AppModules
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddHttpClient();

            // Add Pet Owner api

            services.AddPetOwnerApi(configuration.GetSection("PetOwner"));

            return services;
        }
    }
}
