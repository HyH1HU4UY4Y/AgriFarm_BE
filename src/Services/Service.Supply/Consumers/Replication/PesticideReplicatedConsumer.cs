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
    public class PesticideReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmPesticide>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<PesticideReplicatedConsumer> _logger;

        public PesticideReplicatedConsumer(IMapper mapper,
            IMediator mediator, 
            ILogger<PesticideReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmPesticide>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmPesticide>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
