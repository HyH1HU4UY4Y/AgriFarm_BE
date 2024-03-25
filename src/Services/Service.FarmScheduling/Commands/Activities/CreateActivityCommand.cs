using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.Commands.Activities.Additions;
using Service.FarmScheduling.DTOs;
using SharedDomain.Defaults;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class CreateActivityCommand: IRequest<ActivityResponse>
    {
        public ActivityCreateRequest Activity { get; set; }
        public Guid SiteId { get; set;}
        
    }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityResponse>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateActivityCommandHandler> _logger;
        private IMediator _mediator;

        public CreateActivityCommandHandler(ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateActivityCommandHandler> logger,
            IMediator mediator)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ActivityResponse> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Activity>(request.Activity);
            item.SiteId = request.SiteId;
            item.StartIn = request.Activity.Duration[0];
            item.EndIn = request.Activity.Duration[1];
            var participants = new List<ActivityParticipant>();
            participants.AddRange(
                request.Activity.Workers.Select(x => new ActivityParticipant
                {
                    ParticipantId = x,
                    Role = ActivityRole.Assignee.ToString()
                }));

            participants.AddRange(
                request.Activity.Inspectors.Select(x => new ActivityParticipant
                {
                    ParticipantId = x,
                    Role = ActivityRole.Inspector.ToString()
                }));
            item.Participants = participants;

           

            switch (request.Activity.AdditionType)
            {
                case AdditionType.Training:
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    await _mediator.Publish(new CreateTrainingAdditionEvent
                    {
                        ActivityId = item.Id,
                        Payload = request.Activity.Addition,
                        ActivityTitle = item.Title
                        
                    });
                    break;
                case AdditionType.Use:
                    item.IsCompletable = true;
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    await _mediator.Publish(new CreateUseAdditionEvent
                    {
                        ActivityId = item.Id,
                        Payload = request.Activity.Addition,
                        SiteId = request.SiteId
                    });
                    break;
                case AdditionType.Harvest:
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    await _mediator.Publish(new CreateHarvestAdditionEvent
                    {
                        ActivityId = item.Id,
                        Payload = request.Activity.Addition
                    });
                    break;
                case AdditionType.Assessment:
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    await _mediator.Publish(new CreateAssessmentAdditionEvent
                    {
                        ActivityId = item.Id,
                        Payload = request.Activity.Addition
                    });
                    break;
                case AdditionType.Treatment:
                    item.IsCompletable = true;
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    await _mediator.Publish(new CreateTreatmentAdditionEvent
                    {
                        ActivityId = item.Id,
                        Payload = request.Activity.Addition
                    });
                    break;
                default:
                    item.IsCompletable = true;
                    await _activities.AddAsync(item);

                    await _unit.SaveChangesAsync(cancellationToken);
                    break;

            }

            return _mapper.Map<ActivityResponse>(item);

        }
    }

}
