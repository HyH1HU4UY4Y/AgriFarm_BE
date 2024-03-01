using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Supply.Commands.Base;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.Supply.Consumers.Replication
{
    public class FertilizeReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmFertilize>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<FertilizeReplicatedConsumer> _logger;

        public FertilizeReplicatedConsumer(IMapper mapper,
            IMediator mediator,
            ILogger<FertilizeReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmFertilize>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmFertilize>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
