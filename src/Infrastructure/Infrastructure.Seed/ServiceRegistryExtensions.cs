using Infrastructure.Seed.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;

namespace Infrastructure.Seed
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<SeedlingContext>(configuration);

            services.AddSQLRepo<SeedlingContext, Site>()
                    .AddSQLRepo<SeedlingContext, FarmSeed>()
                    .AddSQLRepo<SeedlingContext, ComponentState>()
                    ;


            return services;
        }
    }
}
