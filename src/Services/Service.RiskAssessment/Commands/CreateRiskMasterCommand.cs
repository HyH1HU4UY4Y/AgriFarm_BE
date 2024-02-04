using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Commands
{
    public class CreateRiskMasterCommand : IRequest<RiskMasterDTO>
    {
        public RiskMaster? riskMaster { get; set; }
    }

    public class CreateRiskMasterCommandHandler : IRequestHandler<CreateRiskMasterCommand, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private IMapper _mapper;
        private ILogger<CreateRiskMasterCommandHandler> _logger;

        public CreateRiskMasterCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskMaster> repo,
        IMapper mapper, ILogger<CreateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RiskMasterDTO> Handle(CreateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {                
                await _repo.AddAsync(request.riskMaster!);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
               
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<RiskMasterDTO>(null);
                }
                return _mapper.Map<RiskMasterDTO>(request.riskMaster!);
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