using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Persistence.Repositories;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;

namespace SharedApplication.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddDefaultSQLDB<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<TDbContext>(o
                => o.UseNpgsql(configuration.GetConnectionString("NSql")
                 , b => b.MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();

            return services;
        }


        public static IServiceCollection AddSQLRepo<TDbContext, TEntity>(this IServiceCollection services)
            where TDbContext : DbContext where TEntity : BaseEntity
        {
            services.AddScoped<ISQLRepository<TDbContext, TEntity>, SQLRepository<TDbContext, TEntity>>();

            return services;
        }

        public static async Task<WebApplication> EnsureDataInit<TDbContext>(this WebApplication app)
            where TDbContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }
            return app;
        }


    }
}
