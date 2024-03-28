using Infrastructure.RiskAssessment.Context;
using MediatR;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Commands
{
    public class CreateRiskContentCommand: IRequest<bool>
    {
        public Guid riskMappingId { get; set; }
        public List<RiskItemContent>? riskItemContents { get; set; }
    }
    public class CreateRiskContentCommandHandler : IRequestHandler<CreateRiskContentCommand, bool>
    {
        private ISQLRepository<RiskAssessmentContext, RiskItemContent> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private ILogger<CreateRiskMasterCommandHandler> _logger;
        public CreateRiskContentCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskItemContent> repo, ILogger<CreateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _logger = logger;
        }
        public async Task<bool> Handle(CreateRiskContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.riskItemContents == null)
                {
                    return false;
                }
                var riskItemContent = await _repo.GetMany(e => e.RiskMappingId == request.riskMappingId);
                if (riskItemContent != null)
                {
                    await _repo.RawDeleteBatchAsync(riskItemContent);
                    await _context.SaveChangesAsync();
                }
                await _repo.AddBatchAsync(request.riskItemContents);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
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
