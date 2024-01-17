using Infrastructure.FarmSite.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedApplication.MultiTenant;

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
    }

}
