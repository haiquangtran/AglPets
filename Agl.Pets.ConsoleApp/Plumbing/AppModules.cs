using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.Infrastructure.Mapping;
using Agl.Pets.Infrastructure.PetOwners;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;


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

            // Mappings

            services.AddAutoMapper(typeof(PetMappingProfile));

            // Add Pet Owner api

            services.AddPetOwnerApi(configuration.GetSection("PetOwner"));

            // Add services

            AddServices(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ICatOrderer, CatOrderer>();
            services.AddSingleton<IPetPrinter, PetPrinter>();
        }
    }
}
