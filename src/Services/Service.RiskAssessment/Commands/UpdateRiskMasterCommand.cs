﻿using AutoMapper;
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

        /*public async Task<RiskMasterDTO> Handle(UpdateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {                
                await _repo.UpdateAsync(request.riskMaster!);
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
        }*/

        public async Task<RiskMasterDTO> Handle(UpdateRiskMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _repo.GetOne(e => (e.Id == request.riskMaster!.Id && e.IsDeleted == false), 
                    r => r.Include(e => e.RiskItems!).ThenInclude(ri => ri.RiskItemContents!));
                if (item != null)
                {
                    item = request.riskMaster;
                    await _repo.UpdateAsync(item!);
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