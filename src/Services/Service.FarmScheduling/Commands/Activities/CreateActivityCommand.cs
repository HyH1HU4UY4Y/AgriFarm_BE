using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.DTOs;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class CreateActivityCommand: IRequest<Guid>
    {
        public ActivityRequest Activity { get; set; }
    }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateActivityCommandHandler> _logger;

        public CreateActivityCommandHandler(ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateActivityCommandHandler> logger)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
