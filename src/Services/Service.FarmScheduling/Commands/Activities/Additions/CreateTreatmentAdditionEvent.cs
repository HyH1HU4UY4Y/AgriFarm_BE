using AutoMapper;
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
    public class CreateTreatmentAdditionEvent: INotification
    {
        public Guid ActivityId { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }

    public class CreateTreatmentAdditionEventHandler : INotificationHandler<CreateTreatmentAdditionEvent>
    {

        private ISQLRepository<ScheduleContext, Activity> _activities;
        private ISQLRepository<ScheduleContext, BaseComponent> _components;
        private ISQLRepository<ScheduleContext, AdditionOfActivity> _additions;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateTreatmentAdditionEventHandler> _logger;
        private IBus _bus;

        public CreateTreatmentAdditionEventHandler(
            ISQLRepository<ScheduleContext, Activity> activities,
            ISQLRepository<ScheduleContext, AdditionOfActivity> additions,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateTreatmentAdditionEventHandler> logger,
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

        public async Task Handle(CreateTreatmentAdditionEvent notification, CancellationToken cancellationToken)
        {
            Guid componentId = Guid.TryParse(notification.Payload["treatmentItem"].ToString(), out componentId) ? componentId : Guid.Empty;
            string method = notification.Payload["method"].ToString() ?? "not set";

            var item = new TreatmentDetail
            {
                ActivityId = notification.ActivityId,
                AdditionType = AdditionType.Treatment,
                ComponentId = componentId,
                TreatmentDescription = method

            };

            await _additions.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

        }
    }
}
