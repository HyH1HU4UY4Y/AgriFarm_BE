using Infrastructure.RiskAssessment.Context;
using MediatR;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Commands
{
    public class CreateRiskMappingCommand : IRequest<bool>
    {
        public Guid riskMasterId { get; set; }
        public Guid taskId { get; set; }

    }
    public class CreateRiskMappingCommandHandler: IRequestHandler<CreateRiskMappingCommand, bool>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMapping> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private ILogger<CreateRiskMasterCommandHandler> _logger;
        public CreateRiskMappingCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskMapping> repo, ILogger<CreateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _logger = logger;
        }
        public async Task<bool> Handle(CreateRiskMappingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                var riskMapping = new RiskMapping
                {
                    RiskMasterId = request.riskMasterId,
                    TaskId = request.taskId,
                };
                await _repo.AddAsync(riskMapping);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                return rs;
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End insert...");
                return false;
            }
        }

    }

}
