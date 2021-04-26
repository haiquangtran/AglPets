using Agl.Pets.Core.Api;
using Agl.Pets.Infrastructure.Pets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Agl.Pets.Infrastructure.PetOwners
{
    [ExcludeFromCodeCoverage]
    public static class PetOwnerExtensions
    {
        public static void AddPetOwnerApi(this IServiceCollection services, IConfiguration configuration)
        {
            var api = configuration.GetSection("Api");
            var apiOptions = api.Get<ApiOptions>();

            services.AddTransient(p => p.GetRequiredService<IOptions<ApiOptions>>().Value);

            services.AddPetOwnersHttpClient<IPetOwnerHttpClient, PetOwnerHttpClient>(apiOptions);

            services.AddTransient<ICatQueries, CatQueries>();
        }

        private static void AddPetOwnersHttpClient<TClient, TImplementation>(this IServiceCollection services
            , ApiOptions options)
            where TClient : class
            where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>(c =>
            {
                c.BaseAddress =
                    new Uri(options.BaseUrl);
            });
        }
    }
}
