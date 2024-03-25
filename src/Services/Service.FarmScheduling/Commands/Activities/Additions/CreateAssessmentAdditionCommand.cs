using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events.Messages;
using Infrastructure.FarmScheduling.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Defaults;
using SharedDomain.Entities.Risk;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities.Additions
{
    public class CreateAssessmentAdditionEvent: INotification
    {
        public Guid ActivityId { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }

    public class CreateAssessmentAdditionEventHandler : INotificationHandler<CreateAssessmentAdditionEvent>
    {
        private IBus _bus;
        private ISQLRepository<ScheduleContext, AdditionOfActivity> _additions;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateAssessmentAdditionEventHandler> _logger;

        public CreateAssessmentAdditionEventHandler(IBus bus,
            ISQLRepository<ScheduleContext, AdditionOfActivity> additions,
            ILogger<CreateAssessmentAdditionEventHandler> logger,
            IUnitOfWork<ScheduleContext> unit,
            IMapper mapper)
        {
            _bus = bus;
            _additions = additions;
            _logger = logger;
            _unit = unit;
            _mapper = mapper;
        }

        public async Task Handle(CreateAssessmentAdditionEvent notification, CancellationToken cancellationToken)
        {
            Guid checkliskId = Guid.TryParse(notification.Payload["checkList"].ToString(), out checkliskId) ? checkliskId : Guid.Empty;
            if(checkliskId == Guid.Empty)
            {
                throw new BadRequestException("'checkList' field require.");
            }

            var item = new AssessmentDetail
            {
                ActivityId = notification.ActivityId,
                AdditionType = AdditionType.Assessment,
                RiskListId = checkliskId

            };

            await _additions.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);


            await _bus.SendToEndpoint(new IntegrationEventMessage<RiskMapping>(
                new()
                {
                    TaskId = notification.ActivityId,
                    RiskMasterId = checkliskId
                },
                EventState.Add
            ), EventQueue.RiskMappingTrackingQueue);
        }
    }
}
