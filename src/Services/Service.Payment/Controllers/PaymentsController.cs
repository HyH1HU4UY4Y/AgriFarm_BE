using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Payment.Commands;
using Service.Payment.DTOs.PaymentDTOs;
using Mapster;
using Asp.Versioning;

namespace Service.Payment.Controllers
{
    [Route("api/v{version:apiVersion}/payment/")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly VnpayConfig _vnpayConfig;
        private IConfiguration _configuration;

        public PaymentsController(IMediator mediator, 
            IOptions<VnpayConfig> vnpayConfigOptions, IConfiguration configuration)
        {
            this._mediator = mediator;
            this._vnpayConfig = vnpayConfigOptions.Value;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("vnpay-return")]
        public async Task<IActionResult> VnpayReturn([FromQuery] VnpayPayResponse response)
        {
            string urlRedirect = _configuration["Vnpay:RedirectUrl"];
            string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDTO();
            var processResult = await _mediator.Send(response.Adapt<ProcessVnpayPaymentReturn>());

            if (processResult != (null, null))
            {
                returnModel = processResult.Item1 as PaymentReturnDTO;
                returnUrl = processResult.Item2 as string;
            }

            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);

            if (!string.IsNullOrEmpty(urlRedirect))
            {
                return Redirect(urlRedirect+"?id=" + response.id.ToString());
            }

            return Redirect("https://devfarm.vercel.app/en/error");
      
        }


        [HttpPost("add")]
        public async Task<IActionResult> InsertPayment([FromBody] PaymentInsertRequest request)
        {
            PaymentInsertResponse response = new PaymentInsertResponse();
            try
            {
                var rs = await _mediator.Send(new CreatePaymentCommand
                {
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
                    Signature = request.Signature,

                    IdRegisterForm = request.IdRegisterForm!,

                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Insert fail!"
                    };
                }
                else
                {
                    response.Id = rs.Id;
                    response.PaymentUrl = rs.PaymentUrl;
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Insert fail!"
                };
            }
            return Ok(response);
        }

       
    }
}
