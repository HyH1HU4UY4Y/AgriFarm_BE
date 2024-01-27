using Infrastructure.Disease.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.Diagnosis;

namespace Infrastructure.Disease
{
    
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<DiseaseContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                .AddSQLRepo<DiseaseContext, DiseaseInfo > ()
                .AddSQLRepo<DiseaseContext, DiseaseDiagnosis > ();


            return services;
        }
    }
}
