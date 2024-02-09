using AutoMapper;
using Azure.Messaging;
using Infrastructure.Payment.Context;
using MediatR;
using Microsoft.Data.SqlClient;
using Service.Payment.DTOs.MerchantDTOs;
using SharedApplication.Pagination.Models;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Pay;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.Data;

namespace Service.Payment.Commands.MerchantCommands
{
    public class CreateMerchantCommand : IRequest<MerchantDTO>
    {
        public string? MerchantName { get; set; } = string.Empty;
        public string? MerchantWebLink { get; set; } = string.Empty;
        public string? MerchantIpnUrl { get; set; } = string.Empty;
        public string? MerchantReturnUrl { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }

    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, MerchantDTO>
    {
        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IUnitOfWork<PaymentContext> _context;
        private IMapper _mapper;
        private ILogger<CreateMerchantCommandHandler> _logger;

        public CreateMerchantCommandHandler(IUnitOfWork<PaymentContext> context, ISQLRepository<PaymentContext, Merchant> repo,
            IMapper mapper, ILogger<CreateMerchantCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MerchantDTO> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                var secretKey = Guid.NewGuid().ToString();
                var isActive = false;
                MerchantDTO merchant = new MerchantDTO
                {
                    MerchantName = request.MerchantName!,
                    MerchantWebLink = request.MerchantWebLink!,
                    MerchantIpnUrl = request.MerchantIpnUrl!,
                    MerchantReturnUrl = request.MerchantReturnUrl!,
                    SecretKey = secretKey,
                    IsActive = isActive,
                    CreatedBy = request.CreatedBy!,
                };
                await _repo.AddAsync(merchant);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<MerchantDTO>(null);
                }
                return merchant;
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
