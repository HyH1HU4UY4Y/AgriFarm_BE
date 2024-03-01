using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.PreHarvest;
using Infrastructure.Supply.Contexts;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Repositories.Base;
using Infrastructure.Supply.Commands.Base;


using MediatR;
using SharedDomain.Defaults;
using System.Reflection;

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

            services.AddMediatrHandlersExplicitly();

            return services;
        }

        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            services
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmSoil>, Guid>, SaveComponentCommandHandler<FarmSoil>>()
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmSeed>, Guid>, SaveComponentCommandHandler<FarmSeed>>()
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmEquipment>, Guid>, SaveComponentCommandHandler<FarmEquipment>>()
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmWater>, Guid>, SaveComponentCommandHandler<FarmWater>>()
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmPesticide>, Guid>, SaveComponentCommandHandler<FarmPesticide>>()
                .AddTransient<IRequestHandler<SaveComponentCommand<FarmFertilize>, Guid>, SaveComponentCommandHandler<FarmFertilize>>()
                ;

            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            
            var db = scope.ServiceProvider.GetRequiredService<SupplyContext>();
            var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork<SupplyContext>>();

            if(!db.Suppliers.Any())
            {
                var components = new BaseComponent[]
                {
                    new FarmPesticide()
                    {
                        Description = "pes",
                        Name = "fffff",
                        SiteId = new Guid(TempData.FarmId)
                    },
                    new FarmFertilize()
                    {
                        Description = "fer",
                        Name = "fffff",
                        SiteId = new Guid(TempData.FarmId)
                    },
                    new FarmEquipment()
                    {
                        Description = "equip",
                        Name = "fffff",
                        SiteId = new Guid(TempData.FarmId)
                    },
                    new FarmSoil()
                    {
                        Description = "adasda",
                        Name = "wqewqq",
                        SiteId = new Guid(TempData.FarmId)
                    },
                    new FarmSeed()
                    {
                        Description = "seed",
                        Name = "ssaf",
                        SiteId = new Guid(TempData.FarmId)
                    }
                };

                db.Components.AddRange(components);

                var suppliers = new Supplier[]
                {
                    new()
                    {
                        Name = "Store 01",
                        Address = "So 5 Can Tho",
                        CreatedByFarmId = new Guid(TempData.FarmId)
                    },
                    new()
                    {
                        Name = "Store 02",
                        Address = "So 7 Can Tho",
                        CreatedByFarmId = new Guid(TempData.FarmId)
                    }
                };

                db.Suppliers.AddRange(suppliers);

                db.SupplyDetails.AddRange(new SupplyDetail[]
                {
                    new()
                    {
                        ComponentId = components[0].Id,
                        Content = $"Add test component {components[0].Name}.",
                        Quantity = 1000,
                        Unit = "ml",
                        UnitPrice = 5000,
                        SupplierId = suppliers[0].Id,
                        SiteId = new Guid(TempData.FarmId)
                    },
                    new()
                    {
                        ComponentId = components[2].Id,
                        Content = $"Add test component {components[2].Name}.",
                        Quantity = 1,
                        Unit = "per",
                        UnitPrice = 5000,
                        SupplierId = suppliers[1].Id,
                        SiteId = new Guid(TempData.FarmId)
                    }

                });

                unit.SaveChangesAsync().Wait();
            }

            

            return app;
        }
    }
}
