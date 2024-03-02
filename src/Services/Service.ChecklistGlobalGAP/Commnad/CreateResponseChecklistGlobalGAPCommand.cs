using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Commnad
{
    public class CreateResponseChecklistGlobalGAPCommand : IRequest<List<ChecklistItemResponseDTO>>
    {
        public ChecklistGlobalGAPCreateResponseRequest? checklistItemResponses { get; set; }
    }
    public class CreateResponseChecklistGlobalGAPCommandHandler : IRequestHandler<CreateResponseChecklistGlobalGAPCommand, List<ChecklistItemResponseDTO>>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistItemResponse> _repo;
        private readonly IUnitOfWork<ChecklistGlobalGAPContext> _context;
        private IMapper _mapper;
        private ILogger<CreateResponseChecklistGlobalGAPCommandHandler> _logger;

        public CreateResponseChecklistGlobalGAPCommandHandler(IUnitOfWork<ChecklistGlobalGAPContext> context, ISQLRepository<ChecklistGlobalGAPContext, ChecklistItemResponse> repo,
            IMapper mapper, ILogger<CreateResponseChecklistGlobalGAPCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<ChecklistItemResponseDTO>> Handle(CreateResponseChecklistGlobalGAPCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                if (request.checklistItemResponses!.checklistItemResponses!.Count > 0)
                {
                    List<ChecklistItemResponse> listInsert = new List<ChecklistItemResponse>();
                    foreach (var item in request.checklistItemResponses!.checklistItemResponses!)
                    {
                        listInsert.Add(new ChecklistItemResponse
                        {
                            ChecklistItemId = item.checklistItemId,
                            ChecklistMappingId = request.checklistItemResponses.checklistMappingId,
                            Level = item.level,
                            Result = item.result,
                            Note = item.note,
                            Attachment = item.attachment
                        });
                    }
                    var listItemResponse = await _repo.GetMany(e => e.ChecklistMappingId == request.checklistItemResponses.checklistMappingId);
                    await _repo.RawDeleteBatchAsync(listItemResponse!.ToList());
                    await _repo.AddBatchAsync(listInsert);
                    var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                    _logger.LogInformation("End insert...");
                    if (!rs)
                    {
                        return _mapper.Map<List<ChecklistItemResponseDTO>>(null);
                    }
                    return _mapper.Map<List<ChecklistItemResponseDTO>>(listInsert);
                } else
                {
                    return _mapper.Map<List<ChecklistItemResponseDTO>>(null);
                }
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End insert...");
                throw new NotFoundException("Insert fail!");
            }
        }
    }
}
