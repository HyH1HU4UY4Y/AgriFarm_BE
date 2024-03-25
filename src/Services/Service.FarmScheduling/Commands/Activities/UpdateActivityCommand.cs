using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.DTOs;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class UpdateActivityCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public ActivityCreateRequest Activity { get; set; }
    }

    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Guid>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<UpdateActivityCommandHandler> _logger;

        public UpdateActivityCommandHandler(ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<UpdateActivityCommandHandler> logger)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public Task<Guid> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
