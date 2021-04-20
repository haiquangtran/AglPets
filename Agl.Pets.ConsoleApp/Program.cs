using Agl.Pets.ConsoleApp.Cats;
using Agl.Pets.ConsoleApp.Plumbing;
using Agl.Pets.Infrastructure.PetOwners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Agl.Pets.ConsoleApp
{
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
                Console.WriteLine("Loading cat pet owners...Please wait");

                using IServiceScope serviceScope = host.Services.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;
                IPetOwnerHttpClient petOwnerClient = provider.GetRequiredService<IPetOwnerHttpClient>();

                var petOwners = await petOwnerClient.GetPetOwners();

                // Print Owners and Cats

                CatPrinter.PrintOwnersAndCats(petOwners);
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
