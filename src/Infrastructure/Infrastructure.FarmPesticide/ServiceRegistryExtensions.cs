using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;


using Infrastructure.Pesticide.Contexts;


namespace Infrastructure.Pesticide
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmPesticideContext>(configuration);

            services.AddSQLRepo<FarmPesticideContext, Site>()
                    .AddSQLRepo<FarmPesticideContext, FarmPesticide>()
                    .AddSQLRepo<FarmPesticideContext, ComponentState>()
                    ;


            return services;
        }
    }
}
