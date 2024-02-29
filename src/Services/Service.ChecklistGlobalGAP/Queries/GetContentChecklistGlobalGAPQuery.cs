using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Queries
{
    public class GetContentChecklistGlobalGAPQuery : IRequest<ChecklistMappingDTO>
    {
        public Guid checklistMappingId { get; set; }
    }
    public class GetContentChecklistGlobalGAPQueryHandler : IRequestHandler<GetContentChecklistGlobalGAPQuery, ChecklistMappingDTO>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> _repo;
        private readonly IMapper _mapper;
        public GetContentChecklistGlobalGAPQueryHandler(IMapper mapper, ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ChecklistMappingDTO> Handle(GetContentChecklistGlobalGAPQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => (e.IsDeleted == false) && (e.Id == request.checklistMappingId), r => r.Include(m => m.ChecklistMaster)!.ThenInclude(n => n!.ChecklistItems)!.ThenInclude(r => r.ChecklistItemResponses)!);
            return _mapper.Map<ChecklistMappingDTO>(rs);
        }
    }
}
