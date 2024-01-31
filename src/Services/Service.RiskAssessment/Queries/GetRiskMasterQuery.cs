using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskMasterQuery : IRequest<List<RiskMasterDTO>>
    {
        public string? keyword { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
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
            var rs = await _repo.GetMany();

            if (rs != null)
            {
                // Search by keyword
                if (request.keyword != null)
                {
                    rs = rs.Where(k => (k.RiskName!.Contains(request.keyword) ||
                                k.RiskDescription!.Contains(request.keyword))
                        ).ToList();
                }
                // Search by date
                DateTime dateFrom;
                DateTime dateTo;
                /*if (DateTime.TryParse(request.searchDateFrom, out dateFrom))
                {
                    rs = rs.Where(k => k.CreatedDate >= dateFrom).ToList();
                }
                if (DateTime.TryParse(request.searchDateTo, out dateTo))
                {
                    rs = rs.Where(k => k.CreatedDate <= dateTo).ToList();
                }*/
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
            }
            return _mapper.Map<List<RiskMasterDTO>>(rs);

        }
    }
}
