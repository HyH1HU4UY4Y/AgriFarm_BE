using Infrastructure.FarmScheduling.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Training;
using Infrastructure.Common.Replication;
using SharedDomain.Entities.Users;
using Microsoft.AspNetCore.Builder;
using SharedDomain.Defaults.Temps;

namespace Infrastructure.FarmScheduling
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<ScheduleContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                    .AddSQLRepo<ScheduleContext, CultivationSeason>()
                    .AddSQLRepo<ScheduleContext, Activity>()
                    .AddSQLRepo<ScheduleContext, Tag>()
                    .AddSQLRepo<ScheduleContext, AdditionOfActivity>()
                    .AddSQLRepo<ScheduleContext, UsingDetail>()
                    .AddSQLRepo<ScheduleContext, HarvestDetail>()
                    .AddSQLRepo<ScheduleContext, TrainingDetail>()
                    .AddSQLRepo<ScheduleContext, TreatmentDetail>()
                    .AddSQLRepo<ScheduleContext, BaseComponent>()
                    .AddSQLRepo<ScheduleContext, FarmSoil>()
                    .AddSQLRepo<ScheduleContext, ActivityParticipant>()
                    .AddSQLRepo<ScheduleContext, MinimalUserInfo>()
                    ;

            services.AddMediatrHandlersExplicitly();

            return services;
        }

        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            services
                .AddComponentReplicateCommand<FarmSoil, ScheduleContext>()
                .AddComponentReplicateCommand<FarmSeed, ScheduleContext>()
                .AddComponentReplicateCommand<FarmEquipment, ScheduleContext>()
                .AddComponentReplicateCommand<FarmWater, ScheduleContext>()
                .AddComponentReplicateCommand<FarmPesticide, ScheduleContext>()
                .AddComponentReplicateCommand<FarmFertilize, ScheduleContext>()
                .AddEntityReplicateCommand<MinimalUserInfo, ScheduleContext>()

                ;

            return services;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ScheduleContext>();

            db.Database.EnsureCreated();
            if (!db.Seasons.Any())
            {
                db.Seasons.AddRange(TempData.Season1, TempData.Season2);
                db.Components.AddRange(
                    TempData.Land1, TempData.Land2,
                    TempData.Seed1, TempData.Seed2
                    
                    );
                db.Activities.AddRange(
                    TempData.Activity1,
                    TempData.Activity2,
                    TempData.Activity3,
                    TempData.Activity4
                    );
                db.SaveChanges();
            }
            

            return app;
        }
    }
}