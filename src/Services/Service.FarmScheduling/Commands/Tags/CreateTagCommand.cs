using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.Queries.Tags;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Commands.Tags
{
    public class CreateTagCommand : IRequest<Guid>
    {
        public string Tag { get; set; }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
    {
        private ISQLRepository<ScheduleContext, Tag> _tags;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<CreateTagCommandHandler> _logger;

        public CreateTagCommandHandler(ISQLRepository<ScheduleContext, Tag> tags,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<CreateTagCommandHandler> logger)
        {
            _tags = tags;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var item = new Tag
            {
                Title = request.Tag
            };

            await _tags.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
