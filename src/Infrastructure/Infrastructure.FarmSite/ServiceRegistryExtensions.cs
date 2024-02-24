using Infrastructure.FarmSite.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.FarmComponents.Others;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Defaults;

namespace Infrastructure.FarmSite
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<SiteContext>(configuration)
                    .AddMultiTenant(configuration);

            services.AddSQLRepo<SiteContext, Site>()
                    .AddSQLRepo<SiteContext, CapitalState>()
                    .AddSQLRepo<SiteContext, Document>()
                    .AddSQLRepo<SiteContext, Subscripton>();
                    
            
            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SiteContext>(); 

            db.Database.EnsureCreated();
            if(!db.Sites.Any(e=>e.Id.ToString() == TempData.FarmId || e.Id.ToString() == TempData.FarmId2)) {
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

                db.SaveChanges();
            }
            

            return app;
        }
    }

}
