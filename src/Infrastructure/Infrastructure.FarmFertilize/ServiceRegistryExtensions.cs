using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Fertilize.Contexts;
using SharedDomain.Entities.FarmComponents.Common;
using Microsoft.AspNetCore.Builder;
using SharedApplication.Times;
using SharedDomain.Defaults;

namespace Infrastructure.Fertilize
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmFertilizeContext>(configuration);

            services.AddSQLRepo<FarmFertilizeContext, Site>()
                    .AddSQLRepo<FarmFertilizeContext, FarmFertilize>()
                    .AddSQLRepo<FarmFertilizeContext, ComponentState>()
                    .AddSQLRepo<FarmFertilizeContext, ComponentProperty>()
                    .AddSQLRepo<FarmFertilizeContext, ReferencedFertilize>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmFertilizeContext>();

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

                var refFer = new ReferencedFertilize
                {
                    Manufactory = "Factory 01",
                    ManufactureDate = "23/10/2011".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                    Name = "Pure Fertilize 01",
                    Description = "A verified pesticide that have great quanlity.",
                    Properties = new()
                    {
                        new("Zn", 20.2, "mg"),
                        new("Fe", 11.22, "mg"),
                        new("Nitrat", 11.22, "mg")
                    }
                };

                db.RefFertilizeInfos.Add(refFer);

                db.FarmFertilizes.AddRange(new FarmFertilize[]
                {
                    new(){

                        Name = "Fertilize 01",

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

                        Name = "Pure Fertilize 01",
                        Unit = "kg",
                        UnitPrice = 200000,
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new List<ComponentProperty>
                        {
                            new()
                            {
                                Name = refFer.Properties[0].Name,
                                Value = refFer.Properties[0].Value,
                                Unit = refFer.Properties[0].Unit
                            },
                            new()
                            {
                                Name = refFer.Properties[1].Name,
                                Value = refFer.Properties[1].Value,
                                Unit = refFer.Properties[1].Unit
                            },
                            new()
                            {
                                Name = refFer.Properties[2].Name,
                                Value = refFer.Properties[2].Value,
                                Unit = refFer.Properties[2].Unit
                            }
                        },
                        ReferenceId = refFer.Id


                    },
                    new(){

                        Name = "Fertilize 02",

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
