using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;


using Infrastructure.Pesticide.Contexts;
using Microsoft.AspNetCore.Builder;
using SharedApplication.Times;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents.Common;


namespace Infrastructure.Pesticide
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<FarmPesticideContext>(configuration);

            services.AddSQLRepo<FarmPesticideContext, Site>()
                    .AddSQLRepo<FarmPesticideContext, FarmPesticide>()
                    .AddSQLRepo<FarmPesticideContext, ReferencedPesticide>()
                    .AddSQLRepo<FarmPesticideContext, ComponentState>()
                    .AddSQLRepo<FarmPesticideContext, ComponentProperty>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FarmPesticideContext>();

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

                var refppp = new ReferencedPesticide
                {
                    Manufactory = "Factory 01",
                    ManufactureDate = "23/10/2011".ToDateTime(DateTimeFormat.dd_MM_yyyy),
                    Name = "Pure PPP 01",
                    Description = "A verified pesticide that have great quanlity.",
                    Properties = new()
                    {
                        new("Zn", 20.2, "mg"),
                        new("Fe", 11.22, "mg"),
                        new("Nitrat", 11.22, "mg")
                    }
                };

                db.RefPesticideInfos.Add(refppp);

                db.FarmPesticides.AddRange(new FarmPesticide[]
                {
                    new(){

                        Name = "PPP 01",

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

                        Name = "Pure PPP 01",
                        Unit = "kg",
                        UnitPrice = 200000,
                        SiteId = new Guid(TempData.FarmId),
                        Properties = new List<ComponentProperty>
                        {
                            new()
                            {
                                Name = refppp.Properties[0].Name,
                                Value = refppp.Properties[0].Value,
                                Unit = refppp.Properties[0].Unit
                            },
                            new()
                            {
                                Name = refppp.Properties[1].Name,
                                Value = refppp.Properties[1].Value,
                                Unit = refppp.Properties[1].Unit
                            },
                            new()
                            {
                                Name = refppp.Properties[2].Name,
                                Value = refppp.Properties[2].Value,
                                Unit = refppp.Properties[2].Unit
                            }
                        },
                        ReferenceId = refppp.Id


                    },
                    new(){

                        Name = "PPP 02",

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
