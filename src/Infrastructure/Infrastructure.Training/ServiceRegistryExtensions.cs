using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Training.Contexts;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Training;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Repositories.Base;
using SharedDomain.Defaults.Temps;

namespace Infrastructure.Training
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMultiTenant(configuration)
                .AddDefaultSQLDB<TrainingContext>(configuration);

            services.AddSQLRepo<TrainingContext, Site>()
                    .AddSQLRepo<TrainingContext, TrainingDetail>()
                    .AddSQLRepo<TrainingContext, TrainingContent>()
                    .AddSQLRepo<TrainingContext, ExpertInfo>()
                    ;


            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<TrainingContext>();
            var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TrainingContext>>();

            if (!db.Trainings.Any())
            {
                db.ExpertInfos.AddRange(TempData.Experts);
                db.Contents.AddRange(TempData.TrainingContents);
                db.Trainings.AddRange(TempData.TrainingDetails);

                db.SaveChanges();
            }
            return app;
        }
    }
}
