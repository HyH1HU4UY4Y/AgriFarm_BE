using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Queries
{
    public class GetListChecklistGlobalGAPQuery : IRequest<List<ChecklistMappingDTO>>
    {
        public Guid userId { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }

    public class GetListChecklistGlobalGAPQueryHandler : IRequestHandler<GetListChecklistGlobalGAPQuery, List<ChecklistMappingDTO>>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> _repo;
        private readonly IMapper _mapper;
        public GetListChecklistGlobalGAPQueryHandler(IMapper mapper, ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<List<ChecklistMappingDTO>> Handle(GetListChecklistGlobalGAPQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany(e => e.UserId == request.userId);
            if (rs != null)
            {
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
            }
            return _mapper.Map<List<ChecklistMappingDTO>>(rs);
        }

    }

}
