using AutoMapper;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using SharedDomain.Entities.ChecklistGlobalGAP;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.ChecklistGlobalGAP.Commnad
{
    public class DeleteListChecklistGlobalGAPCommand : IRequest<bool>
    {
        public Guid checklistMappingId { get; set; }
    }
    public class DeleteListChecklistGlobalGAPCommandHandler : IRequestHandler<DeleteListChecklistGlobalGAPCommand, bool>
    {
        private ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> _repo;
        private readonly IUnitOfWork<ChecklistGlobalGAPContext> _context;
        private IMapper _mapper;
        private ILogger<DeleteListChecklistGlobalGAPCommandHandler> _logger;
        public DeleteListChecklistGlobalGAPCommandHandler(IUnitOfWork<ChecklistGlobalGAPContext> context, ISQLRepository<ChecklistGlobalGAPContext, ChecklistMapping> repo,
            IMapper mapper, ILogger<DeleteListChecklistGlobalGAPCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteListChecklistGlobalGAPCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting insert...");
            try
            {
                var rs = await _repo.GetOne(e => e.Id == request.checklistMappingId);
                if (rs == null)
                {
                    return false;
                }
                await _repo.RawDeleteAsync(rs);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End delete...");
                throw new NotFoundException("Delete fail!");
            }
        }
    }
}
