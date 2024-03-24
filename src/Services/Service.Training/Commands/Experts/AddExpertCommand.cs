using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.Experts
{
    public class AddExpertCommand : IRequest<Guid>
    {
        public ExpertRequest Expert { get; set; }
        public Guid SiteId { get; set; }
    }

    public class AddExpertCommandHandler : IRequestHandler<AddExpertCommand, Guid>
    {
        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddExpertCommandHandler> _logger;

        public AddExpertCommandHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<AddExpertCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddExpertCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with Expert info
            */

            var item = _mapper.Map<ExpertInfo>(request.Expert);
            item.SiteId = request.SiteId;
            item.Certificates = new();

            await _experts.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
