using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class DeleteActivityCommand: IRequest<Guid>
    {
    }

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Guid>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<DeleteActivityCommandHandler> _logger;

        public DeleteActivityCommandHandler(ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<DeleteActivityCommandHandler> logger)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public Task<Guid> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
