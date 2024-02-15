using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Pay;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Payment.Commands.MerchantCommands
{
    public class UpdateMerchantCommand : IRequest<MerchantDTO>
    {
        public Guid Id { get; set; }
        public string? MerchantName { get; set; } 
        public string? MerchantWebLink { get; set; } 
        public string? MerchantIpnUrl { get; set; } 
        public string? MerchantReturnUrl { get; set; } 
        public string? SecretKey { get; set; }
        public Guid? UpdatedBy { get; set; }

    }

    public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, MerchantDTO>
    {
        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IUnitOfWork<PaymentContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateMerchantCommandHandler> _logger;


        public UpdateMerchantCommandHandler(IMapper mapper,
            IUnitOfWork<PaymentContext> unitOfWork,
            ISQLRepository<PaymentContext, Merchant> repo,
            ILogger<UpdateMerchantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _logger = logger;
        }

        public async Task<MerchantDTO> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _repo.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    item.MerchantName = request.MerchantName!;
                    item.MerchantWebLink = request.MerchantWebLink!;
                    item.MerchantIpnUrl = request.MerchantIpnUrl!;
                    item.MerchantReturnUrl = request.MerchantReturnUrl!;
                    item.SecretKey = request.SecretKey!;
                    item.LastUpdatedBy = request.UpdatedBy;
                    await _repo.UpdateAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End update...");
                }
                return _mapper.Map<MerchantDTO>(item);
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
