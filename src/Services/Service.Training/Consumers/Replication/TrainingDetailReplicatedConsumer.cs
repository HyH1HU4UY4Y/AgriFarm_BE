using AutoMapper;
using EventBus.Events.Messages;
using MassTransit;
using MediatR;
using Service.Training.Commands.TrainingDetails;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Additions;

namespace Service.Supply.Consumers.Replication
{
    public class TrainingDetailReplicatedConsumer : IConsumer<IntegrationEventMessage<TrainingDetail>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<TrainingDetailReplicatedConsumer> _logger;

        public TrainingDetailReplicatedConsumer(IMapper mapper,
            IMediator mediator, 
            ILogger<TrainingDetailReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<TrainingDetail>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new AddTrainingDetailCommand()
            {
                ActivityId = detail.ActivityId,
                ContentId = detail.ContentId,
                ExpertId = detail.ExpertId,
                AdditionType = AdditionType.Training,
                Description = detail.Description,
                Id = detail.Id
            });
        }
    }
}
