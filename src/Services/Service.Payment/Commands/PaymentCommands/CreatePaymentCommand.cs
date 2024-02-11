using AutoMapper;
using Infrastructure.Payment.Context;
using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Request;
using MediatR;
using Microsoft.Extensions.Options;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.Interface;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Pay;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Commands
{
    public class CreatePaymentCommand : IRequest<PaymentDTO>
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public Guid PaymentRefId { get; set; }
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        public string? PaymentLanguage { get; set; } = string.Empty;
        public Guid? MerchantId { get; set; } 
        public Guid? PaymentDestinationId { get; set; }
        public Guid? CreatedBy { get; set; }

        public string? Signature { get; set; } = string.Empty;
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDTO>
    {
        private ISQLRepository<PaymentContext, Paymentt> _repo;
        private readonly IUnitOfWork<PaymentContext> _context;
        private IMapper _mapper;
        private ILogger<CreatePaymentCommandHandler> _logger;
        private readonly VnpayConfig _vnpayConfig;
        private readonly ICurrentUserService _currentUserService;



        public CreatePaymentCommandHandler(IUnitOfWork<PaymentContext> context, ISQLRepository<PaymentContext, Paymentt> repo,
            IMapper mapper, ILogger<CreatePaymentCommandHandler> logger, IOptions<VnpayConfig> vnpayConfigOptions, ICurrentUserService currentUserService)
        {
            _context = context;
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _vnpayConfig = vnpayConfigOptions.Value;
            _currentUserService = currentUserService;
        }

        public async Task<PaymentDTO> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                var id = Guid.NewGuid();
                var vnpayPayRequest = new VnpayPayRequest(_vnpayConfig.Version,
                                _vnpayConfig.TmnCode, DateTime.Now, _currentUserService.IpAddress ?? string.Empty, request.RequiredAmount ?? 0, request.PaymentCurrency ?? string.Empty,
                                "other", request.PaymentContent ?? string.Empty, _vnpayConfig.ReturnUrl, /*"166117"*/ id.ToString());
                var paymentUrl = vnpayPayRequest.GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);


                PaymentDTO payment = new PaymentDTO
                {
                    Id = id,
                    PaymentContent = request.PaymentContent!,
                    PaymentCurrency = request.PaymentCurrency!,
                    PaymentRefId = request.PaymentRefId!,
                    RequiredAmount = request.RequiredAmount!,
                    PaymentDate = request.PaymentDate!,
                    ExpireDate = request.ExpireDate!,
                    PaymentLanguage = request.PaymentLanguage!,
                    MerchantId = request.MerchantId!,
                    PaymentDestinationId = request.PaymentDestinationId!,
                    CreatedBy = request.CreatedBy!,
                    PaymentUrl = paymentUrl,
                    Signature = request.Signature!,
                };

                var item = _mapper.Map<Paymentt>(payment);
                await _repo.AddAsync(item);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<PaymentDTO>(null);
                }
                return payment;
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
