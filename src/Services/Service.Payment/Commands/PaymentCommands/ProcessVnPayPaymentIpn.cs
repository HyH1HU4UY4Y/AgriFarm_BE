using AutoMapper;
using Infrastructure.Payment.Context;
using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Response;
using MediatR;
using Microsoft.Extensions.Options;
using SharedDomain.Repositories.Base;
using SharedDomain.Entities.Pay;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentTransactionDTOs;
using Newtonsoft.Json;

namespace Service.Payment.Commands
{
    public class ProcessVnPayPaymentIpn : VnpayPayResponse, IRequest<VnpayPayIpnResponse>
    {

    }

    public class ProcessVnPayPaymentIpnHandler : IRequestHandler<ProcessVnPayPaymentIpn, VnpayPayIpnResponse>
    {
        private readonly VnpayConfig _vnpayConfig;
        private ISQLRepository<PaymentContext, PaymentTransaction> _transaction;
        private ISQLRepository<PaymentContext, Paymentt> _payment;
        private readonly IUnitOfWork<PaymentContext> _context;
        private IMapper _mapper;

        public ProcessVnPayPaymentIpnHandler(IOptions<VnpayConfig> vnpayConfigOptions, IUnitOfWork<PaymentContext> context,
            ISQLRepository<PaymentContext, PaymentTransaction> transaction, ISQLRepository<PaymentContext, Paymentt> payment, IMapper mapper)
        {
            this._vnpayConfig = vnpayConfigOptions.Value;
            _context = context;
            _transaction = transaction;
            _payment = payment;
            _mapper = mapper;
        }
        public async Task<VnpayPayIpnResponse> Handle(
            ProcessVnPayPaymentIpn request, CancellationToken cancellationToken)
        {
            var result = new VnpayPayIpnResponse();
            var resultData = new VnpayPayIpnResponse();

            try
            {
                var isValidSignature = request.IsValidSignature(_vnpayConfig.HashSecret);
                if (isValidSignature)
                {
                    var payment = await _payment.GetOne(e => e.Id.ToString() == request.vnp_TxnRef);

                    if (payment != null)
                    {
                        if(payment.RequiredAmount == (request.vnp_Amount / 100))
                        {
                            if(payment.PaymentStatus != "0")
                            {
                                string message = "";
                                string status = "";

                                if (request.vnp_ResponseCode == "00" &&
                                   request.vnp_TransactionStatus == "00")
                                {
                                    status = "0";
                                    message = "Tran success";
                                }
                                else
                                {
                                    status = "-1";
                                    message = "Tran error";
                                }

                                //Insert database
                                PaymentTransactionDTO transaction = new PaymentTransactionDTO
                                {
                                    TranMessage = request.message![0],
                                    TranPayload = JsonConvert.SerializeObject(request),
                                    TranStatus = status,
                                    TranAmount = request.vnp_Amount,
                                    TranDate = DateTime.Now,
                                    PaymentId = Guid.Parse(request.vnp_TxnRef),
                                    //CreatedBy = request.Cre!,
                                };
                                await _transaction.AddAsync(transaction);
                                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;

                                //Confirm success
                                resultData.Set("00", "Confirm success");
                            }
                            else
                            {
                                resultData.Set("02", "Order already confirmed");
                            }
                        }
                        else
                        {
                            resultData.Set("04", "Invalid amount");
                        }
                    }
                    else
                    {
                        resultData.Set("01", "Order not found");
                    }
                }
                else
                {
                    resultData.Set("97", "Invalid signature");
                }
            }
            catch (Exception ex)
            {
                resultData.Set("99", "Input required data");
            }

            result = resultData;
            return result;
        }
    }
}
