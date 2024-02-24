using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using Infrastructure.Equipment.Contexts;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using SharedDomain.Defaults;

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

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmEquipmentContext>();

            db.Database.EnsureCreated();
            if (!db.Sites.Any(e => e.Id.ToString() == TempData.FarmId))
            {

                db.Sites.Add(new()
                {
                    Id = new Guid(TempData.FarmId),
                    Name = "site01",
                    IsActive = true,
                    SiteCode = "site021.abc",

                });

                db.Sites.Add(new()
                {
                    Id = new Guid(TempData.FarmId2),
                    Name = "site02",
                    IsActive = true,
                    SiteCode = "site032.xyz",

                });

                db.FarmEquipments.AddRange(new FarmEquipment[]
                {
                    new(){

                        Name = "Equip 01",
                        UnitPrice = 200,
                        Description = "12131312 213123123 213213213",
                        Unit = "item",
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Prop a",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "Prop b",
                                Value = 10,
                                Unit = "%"
                            }
                        }

                    },new(){

                        Name = "Equip 02",
                        UnitPrice = 200,
                        Description = "@$$$ HDGER 12131312 213123123 213213213",
                        Unit = "item",
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Prop a",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "Prop b",
                                Value = 10,
                                Unit = "%"
                            }
                        }

                    },new(){

                        Name = "Equip 03",
                        UnitPrice = 100,
                        Description = "12131312 2SDFDSF1312 2132ASD YTJHS3",
                        Unit = "item",
                        SiteId = new Guid(TempData.FarmId2),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Prop a",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "Prop b",
                                Value = 10,
                                Unit = "%"
                            }
                        }

                    },
                });

                db.SaveChanges();
            }


            return app;
        }
    }
}
