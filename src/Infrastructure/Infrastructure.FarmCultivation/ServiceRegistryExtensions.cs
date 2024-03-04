using Infrastructure.FarmCultivation.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.FarmComponents;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using SharedDomain.Defaults;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedApplication.Times;
using Infrastructure.Common.Replication;

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
                    .AddSQLRepo<CultivationContext, HarvestProduct>()
                    //.AddSQLRepo<CultivationContext, Activity>()
                    //.AddSQLRepo<CultivationContext, AdditionOfActivity>()
                    //.AddSQLRepo<CultivationContext, BaseComponent>()
                    .AddSQLRepo<CultivationContext, BaseComponent>()
                    .AddSQLRepo<CultivationContext, FarmSoil>()
                    .AddSQLRepo<CultivationContext, FarmSeed>()
                    ;

            services.AddMediatrHandlersExplicitly();

            return services;
        }

        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            services
                .AddComponentReplicateCommand<FarmSoil, CultivationContext>()
                .AddComponentReplicateCommand<FarmSeed, CultivationContext>()
                ;

            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CultivationContext>();

            db.Database.EnsureCreated();
            if (!db.Seasons.Any())
            {

                var lands = new FarmSoil[]
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
                };

                db.Locations.AddRange(lands);

                var seeds = new FarmSeed[]
                {
                    new(){

                        Name = "Seed 01",

                        Unit = "kg",
                        UnitPrice = 200000,
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

                    },
                    new(){

                        Name = "Seed 02",

                        Unit = "kg",
                        UnitPrice = 200000,
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
                };

                db.Seeds.AddRange(seeds);

                var seasons = new CultivationSeason[]
                {
                    new()
                    {
                        SiteId = new Guid(TempData.FarmId),
                        Description = "Very well season",
                        Title = "Spring",
                        StartIn = "01/01/2023".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                        EndIn = "01/05/2023".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                        

                    },
                    new()
                    {
                        SiteId = new Guid(TempData.FarmId),
                        Description = "Very well season",
                        Title = "Spring",
                        StartIn = "01/01/2019".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                        EndIn = "01/05/2019".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                        

                    }
                };

                db.Seasons.AddRange(seasons);


                var products = new HarvestProduct[]
                {
                    new()
                    {
                        SeedId = seeds[0].Id,
                        LandId = lands[0].Id,
                        Name = $"{seeds[0].Name} ({lands[0].Name})",
                        SeasonId = seasons[0].Id,
                        Unit = "kg",
                        TraceItem = new(null, TraceType.QRCODE,"asdasdasdsadsadsadsadsadsdsadsadssdsadsad"),
                        SiteId = new Guid(TempData.FarmId),
                    },
                    new()
                    {
                        SeedId = seeds[1].Id,
                        LandId = lands[0].Id,
                        Name = $"{seeds[1].Name} ({lands[0].Name})",
                        SeasonId = seasons[0].Id,
                        Unit = "kg",
                        SiteId = new Guid(TempData.FarmId),

                    },
                    new()
                    {
                        SeedId = seeds[1].Id,
                        LandId = lands[1].Id,
                        Name = $"{seeds[1].Name} ({lands[1].Name})",
                        SeasonId = seasons[0].Id,
                        Unit = "kg",
                        SiteId = new Guid(TempData.FarmId),

                    },
                    
                };

                db.Products.AddRange(products);

                db.SaveChanges();
            }


            return app;
        }

    }
}
