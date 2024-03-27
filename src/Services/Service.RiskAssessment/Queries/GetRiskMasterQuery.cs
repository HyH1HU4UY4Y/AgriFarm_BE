using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskMasterQuery : IRequest<List<RiskMasterDTO>>
    {
        public string? keyword { get; set; }
        public bool? isDraft { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }

    public class GetRiskMasterQueryyHandler : IRequestHandler<GetRiskMasterQuery, List<RiskMasterDTO>>
    {

        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repo;
        private readonly IMapper _mapper;

        public GetRiskMasterQueryyHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskMaster> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<List<RiskMasterDTO>> Handle(GetRiskMasterQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany(e => (e.IsDeleted == false),
                r => r.Include(e => e.RiskItems!).ThenInclude(ri => ri.RiskItemContents!));

            if (rs != null)
            {
                // Search by keyword
                if (request.keyword != null)
                {
                    rs = rs.Where(k => (k.RiskName!.Contains(request.keyword) ||
                                k.RiskDescription!.Contains(request.keyword))
                        ).ToList();
                }

                //Filter by isDraft
                if (request.isDraft != null)
                {
                    rs = rs.Where(k => k.IsDraft ==  request.isDraft).ToList();
                }

                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
                rs = rs.OrderByDescending(k => k.CreatedDate).ToList();
            }
            return _mapper.Map<List<RiskMasterDTO>>(rs);

        }
    }
}
