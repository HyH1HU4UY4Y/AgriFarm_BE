using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Commnad
{
    public class CreateChecklistGlobalGAPCommand : IRequest<ChecklistMasterDTO>
    {
       public ChecklistMaster? checklistMaster { get; set; }
        public List<ChecklistItem>? checklistItems { get; set; }

    }
    public class CreateChecklistGlobalGAPCommandHandler : IRequestHandler<CreateChecklistGlobalGAPCommand, ChecklistMasterDTO>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistMaster> _repoMaster;
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistItem> _repoItem;
        private readonly IUnitOfWork<ChecklistGlobalGAPContext> _context;
        private IMapper _mapper;
        private ILogger<CreateChecklistGlobalGAPCommandHandler> _logger;
        public CreateChecklistGlobalGAPCommandHandler(IUnitOfWork<ChecklistGlobalGAPContext> context, ISQLRepository<ChecklistGlobalGAPContext, ChecklistMaster> repoMaster,
            ISQLRepository<ChecklistGlobalGAPContext, ChecklistItem> repoItem,
            IMapper mapper, ILogger<CreateChecklistGlobalGAPCommandHandler> logger)
        {
            _context = context;
            _repoMaster = repoMaster;
            _repoItem = repoItem;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ChecklistMasterDTO> Handle(CreateChecklistGlobalGAPCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                if (request.checklistMaster != null && request.checklistItems != null)
                {
                    await _repoMaster.AddAsync(request.checklistMaster);
                    await _repoItem.AddBatchAsync(request.checklistItems);
                    var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                    return _mapper.Map<ChecklistMasterDTO>(request.checklistMaster);
                } else
                {
                    return _mapper.Map<ChecklistMasterDTO>(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End insert...");
                throw new NotFoundException("Insert fail!");
            }
        }
    }
}
