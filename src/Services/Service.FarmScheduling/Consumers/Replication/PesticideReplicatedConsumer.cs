using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.FarmScheduling.Consumers.Replication
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
