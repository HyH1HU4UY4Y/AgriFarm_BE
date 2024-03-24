using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Water.Contexts;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using SharedDomain.Defaults.Temps;

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
                    .AddSQLRepo<FarmWaterContext, ComponentProperty>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmWaterContext>();

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

                db.FarmWater.AddRange(new FarmWater[]
                {
                    new(){

                        Name = "River 01",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "gallon",
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }

                    },new(){

                        Name = "River 02",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "gallon",
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }

                    },new(){

                        Name = "River 03",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "gallon",
                        SiteId = new Guid(TempData.FarmId2),
                        Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
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
