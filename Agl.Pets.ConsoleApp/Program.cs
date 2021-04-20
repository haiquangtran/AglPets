using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.ConsoleApp.Plumbing;
using Agl.Pets.Domain.Pets;
using Agl.Pets.Infrastructure.PetOwners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Agl.Pets.ConsoleApp
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static async Task Main(string[] args)
        {
            // Enable App settings and configuration
            
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env}.json", true, true)
              .AddEnvironmentVariables()
              .Build();

            using IHost host = CreateHostBuilder(args, configuration).Build();

            // Print cats

            try
            {
                var animalType = AnimalTypes.Cat;

                Console.WriteLine($"Loading {animalType} pet owners...Please wait");

                using IServiceScope serviceScope = host.Services.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;
                IPetOwnerHttpClient petOwnerClient = provider.GetRequiredService<IPetOwnerHttpClient>();

                var petOwners = await petOwnerClient.GetPetOwners();

                // Print Owners and Cats

                Console.WriteLine(PetPrinter.GetFormattedOwnersAndPetsText(animalType, petOwners));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, soemthing went wrong, please try again. {ex.Message}");

                throw;
            }

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) => Host.CreateDefaultBuilder(args)
              .ConfigureServices((_, services) => services.ConfigureAppServices(configuration));
    }
}
