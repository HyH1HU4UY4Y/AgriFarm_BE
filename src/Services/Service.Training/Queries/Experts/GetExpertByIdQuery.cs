using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.Experts
{
    public class GetExpertByIdQuery : IRequest<FullExpertResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetExpertByIdQueryHandler : IRequestHandler<GetExpertByIdQuery, FullExpertResponse>
    {

        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetExpertByIdQueryHandler> _logger;

        public GetExpertByIdQueryHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<GetExpertByIdQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<FullExpertResponse> Handle(GetExpertByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _experts.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<FullExpertResponse>(item);
        }
    }
}
