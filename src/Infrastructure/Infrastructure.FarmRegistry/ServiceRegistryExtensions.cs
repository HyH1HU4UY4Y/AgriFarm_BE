using SharedApplication.Persistence;
using Infrastructure.FarmRegistry.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;
using SharedApplication.MultiTenant;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Defaults;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using Infrastructure.Common.Replication.Commands;
using Infrastructure.Common.Replication;

namespace Infrastructure.FarmRegistry
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<RegistrationContext>(configuration);

            services
                .AddSQLRepo<RegistrationContext, FarmRegistration>()
                .AddSQLRepo<RegistrationContext, PackageSolution>()
                .AddSQLRepo<RegistrationContext, Site>()
                .AddSQLRepo<RegistrationContext, MinimalUserInfo>()
                .AddMultiTenant(configuration);

            services.AddMediatrHandlersExplicitly();

            return services;
        }

        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            services
                .AddReplicateCommand<MinimalUserInfo, RegistrationContext>()
                .AddReplicateCommand<Site, RegistrationContext>();

            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RegistrationContext>();

            db.Database.EnsureCreated();
            if (!db.SiteInfos.Any(e => e.Id.ToString() == TempData.FarmId || e.Id.ToString() == TempData.FarmId2))
            {
                db.SiteInfos.Add(new()
                {
                    Id = new Guid(TempData.FarmId),
                    Name = "site01",
                    IsActive = true,
                    SiteCode = "site021.abc",

                });

                db.SiteInfos.Add(new()
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
