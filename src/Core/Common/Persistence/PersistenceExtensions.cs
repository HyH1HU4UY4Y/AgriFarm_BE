using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;

namespace SharedApplication.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddDefaultSQLDB<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(o
                => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                 , b => b.MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();

            return services;
        }


        public static IServiceCollection AddSQLQueryRepo<TDbContext, TEntity>(this IServiceCollection services)
            where TDbContext : DbContext where TEntity : BaseEntity
        {
            services.AddScoped<IQueryRepository<TDbContext, TEntity>, QueryRepository<TDbContext, TEntity>>();

            return services;
        }
        

        public static IServiceCollection AddSQLCommandRepo<TDbContext, TEntity>(this IServiceCollection services)
            where TDbContext : DbContext where TEntity : BaseEntity
        {
            services.AddScoped<ICommandRepository<TDbContext, TEntity>, CommandRepository<TDbContext, TEntity>>();

            return services;
        }



    }
}
