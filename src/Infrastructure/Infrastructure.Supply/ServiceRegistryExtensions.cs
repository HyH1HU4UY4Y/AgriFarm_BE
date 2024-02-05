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
using SharedDomain.Repositories.Base;

namespace Infrastructure.Supply
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<SupplyContext>(configuration);

            services
                    .AddSQLRepo<SupplyContext, Supplier>()
                    .AddSQLRepo<SupplyContext, SupplyDetail>()
                    .AddSQLRepo<SupplyContext, BaseComponent>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            
            var repo = scope.ServiceProvider.GetRequiredService<ISQLRepository<SupplyContext,BaseComponent>>();
            var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork<SupplyContext>>();

            /*repo.AddAsync(new FarmSoil()
            {
                Description = "adasda",
                Name = "wqewqq"
            });

            repo.AddAsync(new FarmSeed()
            {
                Description = "seed",
                Name = "ssaf"
            });*/

            /*repo.AddBatchAsync(new BaseComponent[]
            {
                new FarmPesticide()
                {
                    Description = "pes",
                    Name = "fffff",
                    s
                },
                new FarmFertilize()
                {
                    Description = "fer",
                    Name = "fffff"
                },
                new FarmEquipment()
                {
                    Description = "equip",
                    Name = "fffff"
                }
            });

            unit.SaveChangesAsync().Wait();*/

            return app;
        }
    }
}
