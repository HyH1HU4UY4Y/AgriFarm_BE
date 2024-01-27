using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.PreHarvest;
using Infrastructure.Supply.Contexts;
using SharedDomain.Entities.Users;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Supply
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<SupplyContext>(configuration);

            services.AddSQLRepo<SupplyContext, Site>()
                    .AddSQLRepo<SupplyContext, Supplier>()
                    .AddSQLRepo<SupplyContext, SupplyDetail>()
                    .AddSQLRepo<SupplyContext, MinimalUserInfo>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {


            return app;
        }
    }
}
