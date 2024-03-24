using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.FarmScheduling.Consumers.Replication
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
