using Infrastructure.FarmCultivation.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.FarmComponents;

namespace Infrastructure.FarmCultivation
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<CultivationContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                    .AddSQLRepo<CultivationContext, CultivationSeason>()
                    .AddSQLRepo<CultivationContext, Activity>()
                    .AddSQLRepo<CultivationContext, AdditionOfActivity>()
                    .AddSQLRepo<CultivationContext, BaseComponent>()
                    .AddSQLRepo<CultivationContext, FarmSoil>()
                    .AddSQLRepo<CultivationContext, FarmSeed>()
                    ;



            return services;
        }
    }
}
