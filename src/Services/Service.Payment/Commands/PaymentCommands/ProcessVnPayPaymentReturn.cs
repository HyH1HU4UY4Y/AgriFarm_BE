using Azure.Messaging;
using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Response;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentDTOs;
using Service.Payment.DTOs;
using AutoMapper;
using Infrastructure.Payment.Context;
using Service.Payment.Commands.MerchantCommands;
using SharedDomain.Entities.Pay;
using SharedDomain.Repositories.Base;
using SharedDomain.Exceptions;

namespace Service.Payment.Commands
{
    public class ProcessVnpayPaymentReturn : VnpayPayResponse, IRequest<(PaymentReturnDTO, string)>
    {

    }

    public class ProcessVnpayPaymentReturnHandler: IRequestHandler<ProcessVnpayPaymentReturn, (PaymentReturnDTO, string)>
    {
        private readonly VnpayConfig _vnpayConfig;
        private ISQLRepository<PaymentContext, Paymentt> _payment;
        private ISQLRepository<PaymentContext, Merchant> _merchant;
        private readonly IUnitOfWork<PaymentContext> _context;
        private IMapper _mapper;

        public ProcessVnpayPaymentReturnHandler(IOptions<VnpayConfig> vnpayConfigOptions, IUnitOfWork<PaymentContext> context, 
            ISQLRepository<PaymentContext, Paymentt> payment, ISQLRepository<PaymentContext, Merchant> merchant, IMapper mapper)
        {
            this._vnpayConfig = vnpayConfigOptions.Value;
            _context = context;
            _payment = payment;
            _merchant = merchant;
            _mapper = mapper;
        }
        public async Task<(PaymentReturnDTO, string)> Handle(
            ProcessVnpayPaymentReturn request, CancellationToken cancellationToken)
        {
            string returnUrl = string.Empty;
            var result = (new PaymentReturnDTO(), returnUrl);
            try
            {
                var resultData = new PaymentReturnDTO();
                var isValidSignature = request.IsValidSignature(_vnpayConfig.HashSecret);

                if (isValidSignature)
                {
                    if(request.vnp_ResponseCode == "00")
                    {

                        var payment = await _payment.GetOne(e => e.Id.ToString() == request.vnp_TxnRef);

                        if (payment != null)
                        {
                            var merchant = await _merchant.GetOne(e => e.Id == payment.MerchantId);

                            returnUrl = merchant!.MerchantReturnUrl ?? string.Empty;
                            resultData.PaymentStatus = "00";
                            resultData.PaymentId = payment.Id.ToString();

                            resultData.Signature = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            resultData.PaymentStatus = "11";
                            resultData.PaymentMessage = "Can't find payment at payment service";
                        }
                    }
                    else
                    {
                        resultData.PaymentStatus = "10";
                        resultData.PaymentMessage = "Payment process failed";
                    }
                } 
                else
                {
                    resultData.PaymentStatus = "99";
                    resultData.PaymentMessage = "Invalid signature in response";
                }

            }
            catch (Exception ex)
            {
                throw new NotFoundException("Payment fail!");
            }
            return result;
        }
    }
}
