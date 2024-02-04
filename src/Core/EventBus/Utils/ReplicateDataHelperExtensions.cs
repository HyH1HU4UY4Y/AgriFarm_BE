using EventBus.Defaults;
using EventBus.Messages;
using MassTransit;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Utils
{
    public static class ReplicateDataHelperExtensions
    {
        public static async Task<IBus> ReplicateUser(this IBus bus, Member data, EventState state)
        {
            var message = new IntegrationEventMessage<MinimalUserInfo>(
                new()
                {
                    AvatarImg = data.AvatarImg,
                    FullName = $"{data.FirstName} {data.LastName}",
                    Id = data.Id,
                    SiteId = data.SiteId,
                    UserName = data.UserName,
                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.UserReplicationQueue);

            return bus;
        }

        public static async Task<IBus> ReplicateFarm(this IBus bus, Site data, EventState state)
        {
            var message = new IntegrationEventMessage<Site>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    IsActive = data.IsActive,
                    SiteCode = data.SiteCode,
                    AvatarImg = data.AvatarImg,
                    LogoImg = data.LogoImg
                     
                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.FarmReplicationQueue);

            return bus;
        }
    }
}
