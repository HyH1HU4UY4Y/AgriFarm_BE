using AutoMapper; 
using Infrastructure.Disease.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Queries
{
    public class GetDiseaseDiagnosesQuery : IRequest<List<DiseaseDiagnosesDTO>>
    {
        public string? keyword { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }

    public class GetDiseaseDiagnosesQueryHandler : IRequestHandler<GetDiseaseDiagnosesQuery, List<DiseaseDiagnosesDTO>>
    {

        private ISQLRepository<DiseaseContext, DiseaseDiagnosis> _repo;
        private readonly IMapper _mapper;

        public GetDiseaseDiagnosesQueryHandler(IMapper mapper, ISQLRepository<DiseaseContext, DiseaseDiagnosis> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<List<DiseaseDiagnosesDTO>> Handle(GetDiseaseDiagnosesQuery request, CancellationToken cancellationToken)
        {
            // Query data
            var rs = (await _repo.GetMany(include: e => e.Include(r => r.PlantDisease!)));
            if (rs  != null)
            {
                rs = rs.Where(k => k.IsDeleted == false);
                // Search by keyword
                if (request.keyword != null)
                {
                    rs = rs.Where(k => (k.Description!.Contains(request.keyword) ||
                                k.Feedback!.Contains(request.keyword) ||
                                k.PlantDisease!.DiseaseName!.Contains(request.keyword))
                        );
                }
                // Search by date
                DateTime dateFrom;
                DateTime dateTo;
                if (DateTime.TryParse(request.searchDateFrom, out dateFrom))
                {
                    rs = rs.Where(k => k.CreatedDate >= dateFrom);
                }
                if (DateTime.TryParse(request.searchDateTo, out dateTo))
                {
                    rs = rs.Where(k => k.CreatedDate <= dateTo);
                }
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage);
                }
            }
            return _mapper.Map<List<DiseaseDiagnosesDTO>>(rs);
        }
    }
}
