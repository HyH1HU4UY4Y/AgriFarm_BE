using EventBus.Events;
using EventBus.Messages;
using MassTransit;

namespace Service.Identity.Consumers
{
    public class InitFarmOwnerConsumer : IConsumer<IntegrationEventMessage<InitFarmOwnerEvent>>
    {
        public Task Consume(ConsumeContext<IntegrationEventMessage<InitFarmOwnerEvent>> context)
        {
            throw new NotImplementedException();
        }
    }
}
