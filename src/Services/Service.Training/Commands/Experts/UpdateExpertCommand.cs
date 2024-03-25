using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.Experts
{
    public class UpdateExpertCommand : IRequest<FullExpertResponse>
    {
        public Guid Id { get; set; }
        public ExpertRequest Expert { get; set; }
    }

    public class UpdateExpertCommandHandler : IRequestHandler<UpdateExpertCommand, FullExpertResponse>
    {
        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateExpertCommandHandler> _logger;

        public UpdateExpertCommandHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<UpdateExpertCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<FullExpertResponse> Handle(UpdateExpertCommand request, CancellationToken cancellationToken)
        {
            var item = await _experts.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Expert, item);

            await _experts.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FullExpertResponse>(item);
        }
    }
}
