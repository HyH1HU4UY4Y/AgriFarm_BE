using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetListRiskMappingQuery : IRequest<List<RiskMappingDTO>>
    {
        public Guid taskId { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }
    public class GetListRiskMappingQueryHandler : IRequestHandler<GetListRiskMappingQuery, List<RiskMappingDTO>>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMapping> _repo;
        private readonly IMapper _mapper;
        public GetListRiskMappingQueryHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskMapping> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<List<RiskMappingDTO>> Handle(GetListRiskMappingQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany(r => r.TaskId == request.taskId);
            if (rs != null)
            {
                rs = rs.ToList();
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
            }
            return _mapper.Map<List<RiskMappingDTO>>(rs);
        }
    }
}
