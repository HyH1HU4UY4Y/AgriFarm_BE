using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Activities
{
    public class MarkDoneCommand: IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set;}
    }

    public class MarkDoneCommandHandler : IRequestHandler<MarkDoneCommand>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<MarkDoneCommandHandler> _logger;
        private IMediator _mediator;

        public MarkDoneCommandHandler(
            ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<MarkDoneCommandHandler> logger,
            IMediator mediator)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(MarkDoneCommand request, CancellationToken cancellationToken)
        {
            var item = await _activities.GetOne(e => e.Id == request.Id 
                                                && e.Participants.Any(x=>x.ParticipantId == request.UserId),
                                                ls =>ls.Include(x=>x.Participants)
                                                        .Include(x=>x.Addtions)
                                                ) ;

            if(item == null)
            {
                throw new NotFoundException();
            }
            
            if(!item.IsCompletable)
            {
                throw new BadRequestException("Require complete the task detail.");
            }

            item.IsCompleted = true;
            item.CompletedAt = DateTime.Now;

            await _activities.UpdateAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
