using Infrastructure.ChecklistGlobalGAP.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Infrastructure.ChecklistGlobalGAP
{
    
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<ChecklistGlobalGAPContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                .AddSQLRepo<ChecklistGlobalGAPContext, ChecklistMaster > ()
                .AddSQLRepo<ChecklistGlobalGAPContext, ChecklistItem > ()
                .AddSQLRepo<ChecklistGlobalGAPContext, ChecklistMapping>()
                .AddSQLRepo<ChecklistGlobalGAPContext, ChecklistItemResponse> ();


            return services;
        }
    }
}
