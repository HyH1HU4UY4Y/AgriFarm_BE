using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using Infrastructure.Equipment.Contexts;

namespace Infrastructure.Equipment
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmEquipmentContext>(configuration);

            services.AddSQLRepo<FarmEquipmentContext, FarmEquipment>()
                    .AddSQLRepo<FarmEquipmentContext, Site>()
                    .AddSQLRepo<FarmEquipmentContext, ComponentState>()
                    ;


            return services;
        }
    }
}
