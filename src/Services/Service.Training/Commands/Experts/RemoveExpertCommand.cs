using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.Experts
{
    public class RemoveExpertCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveExpertCommandHandler : IRequestHandler<RemoveExpertCommand, Guid>
    {
        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveExpertCommandHandler> _logger;

        public RemoveExpertCommandHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<RemoveExpertCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveExpertCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _experts.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _experts.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
