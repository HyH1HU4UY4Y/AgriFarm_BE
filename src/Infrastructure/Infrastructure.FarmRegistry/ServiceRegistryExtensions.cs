using SharedApplication.Persistence;
using Infrastructure.FarmRegistry.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Subscribe;
using Infrastructure.Registration.Repositories;

namespace Infrastructure.FarmRegistry
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<RegistrationContext>(configuration);

            services.AddScoped<IRegistryQueryRepo, RegistryQueryRepo>()
                .AddSQLCommandRepo<RegistrationContext, FarmRegistration>();

            services.AddScoped<ISolutionQueryRepo, SolutionQueryRepo>()
                .AddSQLCommandRepo<RegistrationContext, PackageSolution>();

            return services;
        }
    }
}
