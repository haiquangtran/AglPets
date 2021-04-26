using Agl.Pets.ConsoleApp.Pets;
using Agl.Pets.ConsoleApp.Plumbing;
using Agl.Pets.Infrastructure.Pets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.ConsoleApp
{
    [ExcludeFromCodeCoverage]
    public class Program
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

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            // Print cats

            await PrintCats(provider);

            await host.RunAsync();
        }

        public static async Task PrintCats(IServiceProvider provider)
        {
            try
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Loading cat owners...Please wait");
                Console.WriteLine("\n");

                IPetPrinter petPrinter = provider.GetRequiredService<IPetPrinter>();
                ICatQueries catQueries = provider.GetRequiredService<ICatQueries>();
                ICatOrderer catOrderer = provider.GetRequiredService<ICatOrderer>();

                // Get cat owners

                var catOwners = await catQueries.GetCats();
                var catsGroupedByOwnerGender = catOrderer.GetCatsGroupedByOwnerGender(catOwners);

                // Print cat names grouped by owner's gender

                var catsByOwnerGender = new StringBuilder();
                foreach (var owner in catsGroupedByOwnerGender)
                {
                    catsByOwnerGender.Append(petPrinter.FormatPetNames(owner.OwnerGender, owner.Cats));
                }

                Console.WriteLine(catsByOwnerGender.ToString());
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
