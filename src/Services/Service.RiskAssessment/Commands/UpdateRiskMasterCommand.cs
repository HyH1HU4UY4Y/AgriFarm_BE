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
        public RiskMaster? riskMaster { get; set; }
    }

    public class UpdateRiskMasterCommandHandler : IRequestHandler<UpdateRiskMasterCommand, RiskMasterDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMaster> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private IMapper _mapper;
        private ILogger<UpdateRiskMasterCommandHandler> _logger;

        public UpdateRiskMasterCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskMaster> repo,
         IMapper mapper, ILogger<UpdateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RiskMasterDTO> Handle(UpdateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _repo.GetOne(e => (e.Id == request.riskMaster!.Id && e.IsDeleted == false),
                    r => r.Include(e => e.RiskItems!).ThenInclude(ri => ri.RiskItemContents!));

                if (item != null)
                {
                    await _repo.RawDeleteAsync(item);
                    await _context.SaveChangesAsync();
                    await _repo.AddAsync(request.riskMaster!);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("End update...");
      
                }
                return _mapper.Map<RiskMasterDTO>(item);
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