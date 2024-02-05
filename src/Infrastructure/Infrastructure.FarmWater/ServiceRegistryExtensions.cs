using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Water.Contexts;

namespace Infrastructure.Water
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmWaterContext>(configuration);

            services.AddSQLRepo<FarmWaterContext, Site>()
                    .AddSQLRepo<FarmWaterContext, FarmWater>()
                    .AddSQLRepo<FarmWaterContext, ComponentState>()
                    ;


            return services;
        }
    }
}
