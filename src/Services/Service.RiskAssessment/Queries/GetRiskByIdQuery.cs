using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskByIdQuery : IRequest<RiskMasterDTO>
    {
        public Guid RiskMasterId { get; set; }
    }
    public class GetRiskByIdQueryHandler : IRequestHandler<GetRiskByIdQuery, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repo;
        private readonly IMapper _mapper;

        public GetRiskByIdQueryHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskMaster> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<RiskMasterDTO> Handle(GetRiskByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => (e.Id == request.RiskMasterId && e.IsDeleted == false), 
                r => r.Include(m => m!.RiskItems)!.ThenInclude(ri => ri.RiskItemContents!));
            if (rs?.RiskItems != null)
            {
                rs.RiskItems = rs.RiskItems.OrderBy(r => r.LastModify!).ToList();
            }
            return _mapper.Map<RiskMasterDTO>(rs);
        }
    }
}
