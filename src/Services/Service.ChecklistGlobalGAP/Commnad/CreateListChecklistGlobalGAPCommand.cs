using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Commnad
{
    public class CreateListChecklistGlobalGAPCommand : IRequest<ChecklistMappingDTO>
    {
        public Guid userId { get; set; }
        public Guid checklistMasterId { get; set; }

    }
    public class CreateListChecklistGlobalGAPCommandHandler : IRequestHandler<CreateListChecklistGlobalGAPCommand, ChecklistMappingDTO>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> _repo;
        private readonly IUnitOfWork<ChecklistGlobalGAPContext> _context;
        private IMapper _mapper;
        private ILogger<CreateListChecklistGlobalGAPCommandHandler> _logger;
        public CreateListChecklistGlobalGAPCommandHandler(IUnitOfWork<ChecklistGlobalGAPContext> context, ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> repo,
            IMapper mapper, ILogger<CreateListChecklistGlobalGAPCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ChecklistMappingDTO> Handle(CreateListChecklistGlobalGAPCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                ChecklistMappingDTO checkListMapping = new ChecklistMappingDTO
                {
                    UserId = request.userId,
                    ChecklistMasterId = request.checklistMasterId
                };
                await _repo.AddAsync(checkListMapping);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<ChecklistMappingDTO>(null);
                }
                return checkListMapping;
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
