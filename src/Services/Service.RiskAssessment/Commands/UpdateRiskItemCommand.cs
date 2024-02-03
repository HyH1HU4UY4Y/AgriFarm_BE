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
    public class UpdateRiskItemCommand : IRequest<RiskItemDTO>
    {   
        public RiskItem? riskItem { get; set; }
    }

    public class UpdateRiskItemCommandHandler : IRequestHandler<UpdateRiskItemCommand, RiskItemDTO>
    {
        private ISQLRepository<RiskAssessmentContext, RiskItem> _repo;
        private readonly IUnitOfWork<RiskAssessmentContext> _context;
        private IMapper _mapper;
        private ILogger<UpdateRiskMasterCommandHandler> _logger;

        public UpdateRiskItemCommandHandler(IUnitOfWork<RiskAssessmentContext> context, ISQLRepository<RiskAssessmentContext, RiskItem> repo,
        IMapper mapper, ILogger<UpdateRiskMasterCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RiskItemDTO> Handle(UpdateRiskItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _repo.GetOne(e => (e.Id == request.riskItem!.Id && e.IsDeleted == false),
                    r => r.Include(e => e.RiskItemContents!));

                if (item != null)
                {
                    item.Must = request.riskItem!.Must;
                    item.RiskItemDiv = request.riskItem.RiskItemDiv;
                    item.RiskItemTitle = request.riskItem.RiskItemTitle;
                    item.UpdateBy = request.riskItem.UpdateBy;
                    item.RiskItemType = request.riskItem.RiskItemType;
                    
                    await _repo.UpdateAsync(item!);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("End update...");

                }
                return _mapper.Map<RiskItemDTO>(item);
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