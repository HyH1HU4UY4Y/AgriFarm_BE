using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.FarmScheduling.Consumers.Replication
{
    public class WaterReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmWater>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<WaterReplicatedConsumer> _logger;

        public WaterReplicatedConsumer(IMapper mapper,
            IMediator mediator,
            ILogger<WaterReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmWater>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmWater>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
