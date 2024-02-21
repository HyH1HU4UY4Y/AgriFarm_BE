using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using Infrastructure.Soil.Contexts;
using SharedApplication.MultiTenant;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Defaults;
using Newtonsoft.Json;

namespace Infrastructure.Soil
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmSoilContext>(configuration);

            services.AddSQLRepo<FarmSoilContext, Site>()
                    .AddSQLRepo<FarmSoilContext, FarmSoil>()
                    .AddSQLRepo<FarmSoilContext, ComponentState>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmSoilContext>();

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

                db.FarmLands.AddRange(new FarmSoil[]
                {
                    new(){

                        Name = "Land 01",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "m2",
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

                        Name = "Land 02",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "m2",
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

                        Name = "Land 03",
                        Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
                        Acreage = 100,
                        Unit = "m2",
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
