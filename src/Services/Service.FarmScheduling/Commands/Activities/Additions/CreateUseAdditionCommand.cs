using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities.Additions
{
    public class CreateUseAdditionEvent: INotification
    {
        public Guid SiteId { get; set; }
        public Guid ActivityId { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }

    public class CreateUseAdditionEventHandler : INotificationHandler<CreateUseAdditionEvent>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private ISQLRepository<ScheduleContext, BaseComponent> _components;
        private ISQLRepository<ScheduleContext, AdditionOfActivity> _additions;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateUseAdditionEventHandler> _logger;

        public CreateUseAdditionEventHandler(
            ISQLRepository<ScheduleContext, Activity> activities,
            ISQLRepository<ScheduleContext, AdditionOfActivity> additions,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateUseAdditionEventHandler> logger,
            ISQLRepository<ScheduleContext, BaseComponent> components)
        {
            _activities = activities;
            _additions = additions;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _components = components;
        }


        public async Task Handle(CreateUseAdditionEvent notification, CancellationToken cancellationToken)
        {
            string type = notification.Payload["itemType"].ToString() ?? "none";
            Guid itemId = Guid.TryParse(notification.Payload["useItem"].ToString(), out itemId)? itemId: Guid.Empty;
            var item = await _components.GetOne(e=>e.Id == itemId);
            double useVal = double.TryParse(notification.Payload["quantity"].ToString(), out useVal)? useVal: 1;
            string unit = !string.IsNullOrWhiteSpace(notification.Payload["unit"].ToString())? 
                                notification.Payload["unit"].ToString()!
                                : "kg";
            if (item == null)
            {
                throw new NotFoundException($"Component {type} with id {itemId} not found.");
            }

            var addition = new UsingDetail
            {
                ActivityId = notification.ActivityId,
                AdditionType = AdditionType.Use,
                ComponentId = item.Id,
                UseValue = useVal,
                Unit = unit,
            };

            await _additions.AddAsync(addition);
            await _unit.SaveChangesAsync(cancellationToken);
        }
    }
}
