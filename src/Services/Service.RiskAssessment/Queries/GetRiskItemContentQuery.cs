using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskItemContentQuery : IRequest<int>
    {
        public Guid riskMappingId { get; set; }
    }
    public class GetRiskItemContentQueryHandler : IRequestHandler<GetRiskItemContentQuery, int>
    {
        private ISQLRepository<RiskAssessmentContext, RiskItemContent> _repo;
        private readonly IMapper _mapper;
        public GetRiskItemContentQueryHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskItemContent> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<int> Handle(GetRiskItemContentQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany(r => r.RiskMappingId == request.riskMappingId);
            if (rs != null)
            {
                return rs.Count();
            }
            return 0;
        }
    }
}
