using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events.Messages;
using Infrastructure.FarmScheduling.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities.Additions
{
    public class CreateTrainingAdditionEvent: INotification
    {
        public Guid ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }

    public class CreateTrainingAdditionEventHandler : INotificationHandler<CreateTrainingAdditionEvent>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private ISQLRepository<ScheduleContext, BaseComponent> _components;
        private ISQLRepository<ScheduleContext, AdditionOfActivity> _additions;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateTrainingAdditionEventHandler> _logger;
        private IBus _bus;

        public CreateTrainingAdditionEventHandler(
            ISQLRepository<ScheduleContext, Activity> activities,
            ISQLRepository<ScheduleContext, AdditionOfActivity> additions,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateTrainingAdditionEventHandler> logger,
            ISQLRepository<ScheduleContext, BaseComponent> components,
            IBus bus)
        {
            _activities = activities;
            _additions = additions;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _components = components;
            _bus = bus;
        }

        public async Task Handle(CreateTrainingAdditionEvent notification, CancellationToken cancellationToken)
        {
            Guid contentId = Guid.TryParse(notification.Payload["content"].ToString(), out contentId) ? contentId : Guid.Empty;
            Guid expertId = Guid.TryParse(notification.Payload["expert"].ToString(), out expertId) ? expertId : Guid.Empty;



            var item = new TrainingDetail
            {
                ActivityId = notification.ActivityId,
                ContentId = contentId,
                ExpertId = expertId,
                AdditionType = AdditionType.Training,
                Description = "none",
                Title = notification.ActivityTitle

            };

            await _additions.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.SendToEndpoint(new IntegrationEventMessage<TrainingDetail>
            (
                new()
                {
                    ActivityId = item.ActivityId,
                    ContentId = item.ContentId,
                    ExpertId = expertId,
                    AdditionType= AdditionType.Training,
                    Description = item.Description,
                    Id = item.Id,
                },
                EventState.Add
            ), EventQueue.TrainingDetailReplicationQueue);
        }
    }
}
