using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;

using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using Infrastructure.Training.Contexts;
using SharedDomain.Entities.Schedules.Training;

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
    }
}
