using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Commands
{
    public class CreateRiskMasterCommand : IRequest<RiskMasterDTO>
    {
        public RiskMaster? riskMaster { get; set; }
        public List<RiskItem>? riskItems { get; set; } 
    }

    public class CreateRiskMasterCommandHandler : IRequestHandler<CreateRiskMasterCommand, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repoMaster;
        private ISQLRepository<RiskAssessmentContext, RiskItem> _repoItem;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private IMapper _mapper;
        private ILogger<CreateRiskMasterCommandHandler> _logger;

        public CreateRiskMasterCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskMaster> repoMaster, ISQLRepository<RiskAssessmentContext, RiskItem> repoItem,
        IMapper mapper, ILogger<CreateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repoMaster = repoMaster;
            _repoItem = repoItem;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RiskMasterDTO> Handle(CreateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {                
                if (request.riskMaster != null && request.riskItems != null)
                {
                    await _repoMaster.AddAsync(request.riskMaster);
                    await _repoItem.AddBatchAsync(request.riskItems);
                    var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                    if (rs)
                    {
                        _logger.LogInformation("End insert...");
                        return _mapper.Map<RiskMasterDTO>(request.riskMaster);
                    }
                }
                return _mapper.Map<RiskMasterDTO>(null);
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