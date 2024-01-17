using Infrastructure.FarmScheduling.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules.Training;

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
                    .AddSQLRepo<ScheduleContext, CultivationRecord>()
                    .AddSQLRepo<ScheduleContext, Activity>()
                    .AddSQLRepo<ScheduleContext, Tag>()
                    .AddSQLRepo<ScheduleContext, AdditionOfActivity>()
                    .AddSQLRepo<ScheduleContext, ActivityParticipant>()
                    .AddSQLRepo<ScheduleContext, TrainingDetail>()
                    .AddSQLRepo<ScheduleContext, TrainingContent>()
                    .AddSQLRepo<ScheduleContext, BaseComponent>()
                    .AddSQLRepo<ScheduleContext, FarmSoil>()
                    ;



            return services;
        }
    }
}