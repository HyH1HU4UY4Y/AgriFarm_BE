using EventBus.Defaults;
using EventBus.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Utils
{
    public static class ReplicationConsumerHelperExtensions
    {
        public static async Task ProcessReplicate<TDbContext, TEntity>(this ISQLRepository<TDbContext, TEntity> repository, IntegrationEventMessage<TEntity> message)
            where TDbContext : DbContext where TEntity : IBaseEntity<Guid>
        {
            switch (message.State)
            {
                case EventState.Add:
                    await repository.AddAsync(message.Data);
                    break;
                case EventState.Modify:
                    await repository.UpdateAsync(message.Data);
                    break;
                case EventState.SoftDelete:
                    await repository.SoftDeleteAsync(message.Data);
                    break;
                case EventState.RawDelete:
                    await repository.SoftDeleteAsync(message.Data);
                    break;
                default:
                    break;

            }


        }
        public static async Task ProcessComponentReplicate<TDbContext, TComponent>(this ISQLRepository<TDbContext, BaseComponent> repository, IntegrationEventMessage<TComponent> message)
            where TDbContext : DbContext where TComponent: BaseComponent
        {
            switch (message.State)
            {
                case EventState.Add:
                    await repository.AddAsync(message.Data);
                    break;
                case EventState.Modify:
                    await repository.UpdateAsync(message.Data);
                    break;
                case EventState.SoftDelete:
                    await repository.SoftDeleteAsync(message.Data);
                    break;
                case EventState.RawDelete:
                    await repository.SoftDeleteAsync(message.Data);
                    break;
                default:
                    break;

            }


        }
    }
}
