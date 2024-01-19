using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using Infrastructure.Soil.Contexts;
using SharedApplication.MultiTenant;

namespace Infrastructure.Soil
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmSoilContext>(configuration);

            services.AddSQLRepo<FarmSoilContext, Site>()
                    .AddSQLRepo<FarmSoilContext, FarmSoil>()
                    .AddSQLRepo<FarmSoilContext, ComponentState>()
                    ;


            return services;
        }
    }
}
