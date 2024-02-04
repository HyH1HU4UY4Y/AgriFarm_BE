using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.Commands.Tags;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Tags
{
    public class DeleteTagCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Guid>
    {
        private ISQLRepository<ScheduleContext, Tag> _tags;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<DeleteTagCommandHandler> _logger;

        public DeleteTagCommandHandler(ISQLRepository<ScheduleContext, Tag> tags,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<DeleteTagCommandHandler> logger)
        {
            _tags = tags;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<Guid> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var item = await _tags.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _tags.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;

        }
    }
}
