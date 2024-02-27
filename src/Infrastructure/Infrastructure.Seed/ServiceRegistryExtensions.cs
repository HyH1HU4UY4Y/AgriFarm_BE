using Infrastructure.Seed.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedApplication.Times;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using System.Globalization;

namespace Infrastructure.Seed
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmSeedContext>(configuration);

            services.AddSQLRepo<FarmSeedContext, Site>()
                    .AddSQLRepo<FarmSeedContext, FarmSeed>()
                    .AddSQLRepo<FarmSeedContext, ReferencedSeed>()
                    .AddSQLRepo<FarmSeedContext, ComponentState>()
                    .AddSQLRepo<FarmSeedContext, ComponentProperty>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmSeedContext>();

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

                var seed = new ReferencedSeed
                {
                    Manufactory = "Factory 01",
                    ManufactureDate = "23/10/2011".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                    Name = "Pure Seed 01",
                    Description = "A verified rice seed that have great quanlity.",
                    Properties = new()
                    {
                        new("Zn", 20.2, "mg"),
                        new("Fe", 11.22, "mg"),
                        new("Nitrat", 11.22, "mg")
                    }
                };

                db.RefSeedInfos.Add(seed);

                db.FarmSeeds.AddRange(new FarmSeed[]
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

                    },new(){

                        Name = "Pure Seed 01",
                        Unit = "kg",
                        UnitPrice = 200000,
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new List<ComponentProperty>
                        {
                            new()
                            {
                                Name = seed.Properties[0].Name,
                                Value = seed.Properties[0].Value,
                                Unit = seed.Properties[0].Unit
                            },
                            new()
                            {
                                Name = seed.Properties[1].Name,
                                Value = seed.Properties[1].Value,
                                Unit = seed.Properties[1].Unit
                            },
                            new()
                            {
                                Name = seed.Properties[2].Name,
                                Value = seed.Properties[2].Value,
                                Unit = seed.Properties[2].Unit
                            }
                        },
                        ReferenceId = seed.Id


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
                });

                db.SaveChanges();
            }


            return app;
        }
    }

   
}
