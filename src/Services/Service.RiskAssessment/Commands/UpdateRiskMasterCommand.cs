using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.RiskAssessment.DTOs;
using SharedApplication.Persistence;
using SharedDomain.Entities.Risk;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Commands
{
    public class UpdateRiskMasterCommand : IRequest<RiskMasterDTO>
    {   
        public Guid riskMasterId { get; set; }
        public List<RiskItem>? riskItems { get; set; }
        public RiskMaster? riskMaster { get; set; }
    }

    public class UpdateRiskMasterCommandHandler : IRequestHandler<UpdateRiskMasterCommand, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repoMaster;
        private ISQLRepository<RiskAssessmentContext, RiskItem> _repoItem;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private IMapper _mapper;
        private ILogger<UpdateRiskMasterCommandHandler> _logger;

        public UpdateRiskMasterCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskMaster> repoMaster,
            ISQLRepository<RiskAssessmentContext, RiskItem> repoItem,
         IMapper mapper, ILogger<UpdateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repoMaster = repoMaster;
            _repoItem = repoItem;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RiskMasterDTO> Handle(UpdateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                if (request.riskMaster != null && request.riskItems != null)
                {
                    await _repoMaster.AddAsync(request.riskMaster);
                    await _repoItem.AddBatchAsync(request.riskItems);
                    var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                    return _mapper.Map<RiskMasterDTO>(request.riskMaster);
                }
                else
                {
                    return _mapper.Map<RiskMasterDTO>(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End update...");
                throw new NotFoundException("Update fail!");
            }
        }
    }
}