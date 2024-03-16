using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskItemQuery: IRequest<int>
    {
        public Guid riskMasterId { get; set; }
    }
    public class GetRiskItemQueryHandler : IRequestHandler<GetRiskItemQuery, int>
    {
        private ISQLRepository<RiskAssessmentContext, RiskItem> _repo;
        private readonly IMapper _mapper;
        public GetRiskItemQueryHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskItem> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<int> Handle(GetRiskItemQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany(r => r.RiskMasterId == request.riskMasterId);
            if (rs != null)
            {
                return rs.Count();
            }
            return 0;
        }
    }    
}
