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
    public class SeedReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmSeed>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<SeedReplicatedConsumer> _logger;

        public SeedReplicatedConsumer(IMapper mapper,
            IMediator mediator, 
            ILogger<SeedReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmSeed>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmSeed>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
