using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Fertilize.Contexts;

namespace Infrastructure.Fertilize
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmFertilizeContext>(configuration);

            services.AddSQLRepo<FarmFertilizeContext, Site>()
                    .AddSQLRepo<FarmFertilizeContext, FarmFertilize>()
                    .AddSQLRepo<FarmFertilizeContext, ComponentState>()
                    ;


            return services;
        }
    }
}
