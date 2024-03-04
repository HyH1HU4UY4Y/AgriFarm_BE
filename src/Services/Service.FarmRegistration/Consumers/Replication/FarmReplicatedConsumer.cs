using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.Registration.Consumers.Replication
{
    public class FarmReplicatedConsumer : IConsumer<IntegrationEventMessage<Site>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<FarmReplicatedConsumer> _logger;

        public FarmReplicatedConsumer(IMapper mapper,
            IMediator mediator,
            ILogger<FarmReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<Site>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveEntityCommand<Site>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
