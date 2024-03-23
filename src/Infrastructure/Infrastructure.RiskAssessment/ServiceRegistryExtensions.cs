using Infrastructure.RiskAssessment.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.Risk;


namespace Infrastructure.Disease
{
    
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<RiskAssessmentContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                .AddSQLRepo<RiskAssessmentContext, RiskMaster > ()
                .AddSQLRepo<RiskAssessmentContext, RiskItem>()
                .AddSQLRepo<RiskAssessmentContext, RiskItemContent> ()
                .AddSQLRepo<RiskAssessmentContext, RiskMapping>();


            return services;
        }
    }
}
