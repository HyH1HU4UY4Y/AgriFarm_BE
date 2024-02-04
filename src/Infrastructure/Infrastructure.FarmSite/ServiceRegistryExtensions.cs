using Infrastructure.FarmSite.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;

namespace Infrastructure.FarmSite
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<SiteContext>(configuration);

            services.AddSQLCommandRepo<SiteContext, Site>()
                    .AddSQLCommandRepo<SiteContext, CapitalState>()
                    .AddSQLCommandRepo<SiteContext, Document>();
                    
            /*
                        services.AddScoped<ISolutionQueryRepo, SolutionQueryRepo>()
            .AddScoped<IRegistryQueryRepo, RegistryQueryRepo>()
                            
            */

            return services;
        }
    }

}
