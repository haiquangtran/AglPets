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

            await PrintPets(AnimalTypes.Cat, host);

            await host.RunAsync();
        }

        public static async Task PrintPets(string animalType, IHost host)
        {
            try
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Loading {animalType} pet owners...Please wait");
                Console.WriteLine("\n");

                using IServiceScope serviceScope = host.Services.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;

                IPetOwnerHttpClient petOwnerClient = provider.GetRequiredService<IPetOwnerHttpClient>();
                IPetPrinter petPrinter = provider.GetRequiredService<IPetPrinter>();

                var petOwners = await petOwnerClient.GetPetOwners();

                // Print Owners and Pets

                Console.WriteLine(petPrinter.GetFormattedOwnersAndPetsText(animalType, petOwners));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, soemthing went wrong, please try again. {ex.Message}");

                throw;
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) => Host.CreateDefaultBuilder(args)
              .ConfigureServices((_, services) => services.ConfigureAppServices(configuration));
    }
}
