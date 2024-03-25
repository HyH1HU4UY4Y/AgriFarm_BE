using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;

namespace Service.FarmScheduling.Consumers.Replication
{
    public class EquipmentReplicatedConsumer : IConsumer<IntegrationEventMessage<FarmEquipment>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<EquipmentReplicatedConsumer> _logger;

        public EquipmentReplicatedConsumer(IMapper mapper,
            IMediator mediator,
            ILogger<EquipmentReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmEquipment>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveComponentCommand<FarmEquipment>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
