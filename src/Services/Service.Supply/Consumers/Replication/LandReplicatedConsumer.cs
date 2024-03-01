using AutoMapper;
using EventBus.Events.Messages;
using EventBus.Events;
using MassTransit;
using Service.Supply.Commands.Supplies;
using Service.Supply.Consumers.Supplying;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using Infrastructure.Supply.Commands.Base;
using EventBus.Defaults;

namespace Service.Supply.Consumers.Replication
{
    public class LandReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmSoil>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<LandReplicatedConsumer> _logger;

        public LandReplicatedConsumer(IMapper mapper,
            IMediator mediator, 
            ILogger<LandReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmSoil>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmSoil>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
