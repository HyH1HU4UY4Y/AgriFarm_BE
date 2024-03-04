using Infrastructure.Common.Replication.Commands;
using MassTransit.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;

namespace Infrastructure.Common.Replication
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddReplicateCommand<TEntity, TDBContext>(this IServiceCollection services)
            where TEntity : class, IBaseEntity<Guid> where TDBContext: DbContext
        {
            services
                .AddTransient<IRequestHandler<SaveEntityCommand<TEntity>, Guid>, SaveEntityCommandHandler<TEntity, TDBContext>>()
                ;

            return services;
        }
    }
}
