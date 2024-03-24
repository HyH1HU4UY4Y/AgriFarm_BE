using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class DeleteActivityCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
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

        public async Task<Guid> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var item = await _activities.GetOne(e=>e.Id == request.Id);
            if (item == null)
            {
                throw new NotFoundException();

            }
            await _activities.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
