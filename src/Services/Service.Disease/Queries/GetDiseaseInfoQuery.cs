using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Queries
{
    public class GetDiseaseInfoQuery: IRequest<List<DiseaseInfoDTO>>
    {
        public string? keyword { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }

    public class GetAllDiseaseInfosQueryHandler : IRequestHandler<GetDiseaseInfoQuery, List<DiseaseInfoDTO>>
    {
        
        private ISQLRepository<DiseaseContext, DiseaseInfo> _repo;
        private readonly IMapper _mapper;

        public GetAllDiseaseInfosQueryHandler(IMapper mapper, ISQLRepository<DiseaseContext, DiseaseInfo> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<List<DiseaseInfoDTO>> Handle(GetDiseaseInfoQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany();

            if (rs != null)
            {
                // Search by keyword
                if (request.keyword != null)
                {
                    rs = rs.Where(k => (k.DiseaseName!.Contains(request.keyword) ||
                                k.Symptoms!.Contains(request.keyword) ||
                                k.Cause!.Contains(request.keyword))
                        ).ToList();
                }
                // Search by date
                DateTime dateFrom;
                DateTime dateTo;
                if (DateTime.TryParse(request.searchDateFrom, out dateFrom))
                {
                    rs = rs.Where(k => k.CreatedDate >= dateFrom).ToList();
                }
                if (DateTime.TryParse(request.searchDateTo, out dateTo))
                {
                    rs = rs.Where(k => k.CreatedDate <= dateTo).ToList();
                }
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
            }
            return _mapper.Map<List<DiseaseInfoDTO>>(rs);

        }
    }
}
