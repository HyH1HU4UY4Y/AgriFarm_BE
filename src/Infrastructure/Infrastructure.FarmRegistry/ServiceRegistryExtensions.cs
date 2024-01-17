using SharedApplication.Persistence;
using Infrastructure.FarmRegistry.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;

namespace Infrastructure.FarmRegistry
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<RegistrationContext>(configuration);

            services
                .AddSQLRepo<RegistrationContext, FarmRegistration>()
                .AddSQLRepo<RegistrationContext, PackageSolution>();

            return services;
        }
    }
}
