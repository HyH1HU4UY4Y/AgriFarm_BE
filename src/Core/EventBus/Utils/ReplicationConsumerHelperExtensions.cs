using AutoMapper;
using EventBus.Defaults;
using EventBus.Events.Messages;
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
        public static async Task ProcessReplicate<TDbContext, TEntity>(this ISQLRepository<TDbContext, TEntity> repository, TEntity data, EventState state, IMapper? mapper = null)
            where TDbContext : DbContext where TEntity : class, IBaseEntity<Guid>
        {
            TEntity entity = null;
            if (state != EventState.Add)
            {
                entity = repository.GetOne(e => e.Id == data.Id).Result!;
            }

            switch (state)
            {
                case EventState.Add:
                    await repository.AddAsync(data);
                    break;
                case EventState.Modify:
                    if (mapper != null && entity != null)
                    {
                        mapper.Map(data, entity);
                        await repository.UpdateAsync(entity);
                    }
                    else throw new Exception();
                    break;
                case EventState.SoftDelete:
                    await repository.SoftDeleteAsync(entity);
                    break;
                case EventState.RawDelete:
                    await repository.RawDeleteAsync(entity);
                    break;
                default:
                    break;

            }

        }

        public static void ProcessComponentReplicate<TDbContext, TComponent>(this ISQLRepository<TDbContext, BaseComponent> repository, TComponent data, EventState state)
            where TDbContext : DbContext where TComponent: BaseComponent
        {
            TComponent component = null;
            if(state != EventState.Add)
            {
                component = (TComponent?)repository.GetOne(e => e.Id == data.Id).Result!;
            }

            switch (state)
            {
                case EventState.Add:
                    repository.AddAsync(data);
                    break;
                case EventState.Modify:
                    component.Name = data.Name;
                    repository.UpdateAsync(component);
                    break;
                case EventState.SoftDelete:
                    repository.SoftDeleteAsync(component);
                    break;
                case EventState.RawDelete:
                    repository.RawDeleteAsync(component);
                    break;
                default:
                    break;

            }


        }
    }
}
