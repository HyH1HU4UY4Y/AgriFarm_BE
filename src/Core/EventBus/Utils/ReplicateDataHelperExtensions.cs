using EventBus.Defaults;
using EventBus.Events.Messages;
using MassTransit;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;

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
        
        public static async Task<IBus> ReplicateSoil(this IBus bus, FarmSoil data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmSoil>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,

                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.SoilReplicationQueue);

            return bus;
        }

        public static async Task<IBus> ReplicateSeed(this IBus bus, FarmSeed data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmSeed>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,

                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.SeedReplicationQueue);

            return bus;
        }
        
        public static async Task<IBus> ReplicatePesticide(this IBus bus, FarmPesticide data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmPesticide>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,
                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.PesticideReplicationQueue);

            return bus;
        }
        
        public static async Task<IBus> ReplicateFertilize(this IBus bus, FarmFertilize data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmFertilize>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,
                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.FertilizeReplicationQueue);

            return bus;
        }

        public static async Task<IBus> ReplicateWaterSource(this IBus bus, FarmWater data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmWater>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,

                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.WaterReplicationQueue);

            return bus;
        }

        public static async Task<IBus> ReplicateEquipment(this IBus bus, FarmEquipment data, EventState state)
        {
            var message = new IntegrationEventMessage<FarmEquipment>(
                new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SiteId = data.SiteId,
                    IsConsumable = data.IsConsumable,

                },
                state
            );

            await bus.SendToEndpoint(message, EventQueue.EquipmentReplicationQueue);

            return bus;
        }

    }
}
