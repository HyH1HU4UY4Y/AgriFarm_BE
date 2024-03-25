using EventBus.Events.Messages;
using MassTransit;
using MediatR;
using Service.RiskAssessment.Commands;
using SharedDomain.Entities.Risk;
using SharedDomain.Entities.Schedules.Additions;

namespace Service.RiskAssessment.Consumers
{
    public class RiskMappingConsumer : IConsumer<IntegrationEventMessage<RiskMapping>>
    {
        private IMediator _mediator;

        public RiskMappingConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<RiskMapping>> context)
        {
            var data = context.Message.Data;

            await _mediator.Send(new CreateRiskMappingCommand
            {
                riskMasterId = data.RiskMasterId,
                taskId = data.TaskId,
            });
        }
    }
}
