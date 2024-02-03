using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Risk;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Commands
{
    public class DeleteRiskAssessmentCommand : IRequest<RiskMasterDTO>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRiskAssessmentCommandHandler : IRequestHandler<DeleteRiskAssessmentCommand, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteRiskAssessmentCommandHandler> _logger;


        public DeleteRiskAssessmentCommandHandler(IMapper mapper,
            IUnitOfWork<RiskAssessmentContext> unitOfWork,
            ISQLRepository<RiskAssessmentContext, RiskMaster> repo,
            ILogger<DeleteRiskAssessmentCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _logger = logger;
        }

        public async Task<RiskMasterDTO> Handle(DeleteRiskAssessmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting delete...");
            try
            {
                var riskMaster = await _repo.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (riskMaster != null)
                {
                    await _repo.RawDeleteAsync(riskMaster);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End delete...");
                }
                return _mapper.Map<RiskMasterDTO>(riskMaster);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End delete...");
                throw new NotFoundException("Delete fail!");
            }
        }
    }
}
