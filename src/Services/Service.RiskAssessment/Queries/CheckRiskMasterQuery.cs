using Infrastructure.RiskAssessment.Context;
using MediatR;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class CheckRiskMasterQuery : IRequest<bool>
    {
        public Guid RiskMasterId { get; set; }
    }
    public class CheckRiskMasterQueryHandler : IRequestHandler<CheckRiskMasterQuery, bool>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMapping> _repo;

        public CheckRiskMasterQueryHandler(ISQLRepository<RiskAssessmentContext, RiskMapping> repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(CheckRiskMasterQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => (e.RiskMasterId == request.RiskMasterId));
            if (rs != null)
            {
                return true;
            }
            return false;
        }
    }
}
