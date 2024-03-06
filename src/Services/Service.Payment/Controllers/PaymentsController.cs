using Infrastructure.Payment.Utils;
using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Service.Payment.Commands;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentDTOs;
using Service.Payment.Queries.MerchantQueries;
using Service.Payment.Queries.Payment;
using System;
using Mapster;
using Humanizer.Configuration;

namespace Service.Payment.Controllers
{
    [Route("api/payment/")]
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
                return Redirect(urlRedirect);
            }
            else
            {
                return Redirect("http://localhost:3000/vi/error");
            }
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
                    Signature = request.Signature

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

       /* [HttpPut("set-active")]
        public async Task<IActionResult> SetActiveMerchant([FromBody] MerchantSetActiveRequest request)
        {
            MerchantSetActiveResponse response = new MerchantSetActiveResponse();
            try
            {
                var rs = await _mediator.Send(new SetActiveMerchantCommand
                {
                    Id = request.Id,
                    IsActive = request.IsActive
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Update fail!"
                };
            }
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateMerchant([FromBody] MerchantUpdateRequest request)
        {
            MerchantUpdateResponse response = new MerchantUpdateResponse();
            try
            {
                var rs = await _mediator.Send(new UpdateMerchantCommand
                {
                    Id = request.Id,
                    MerchantName = request.MerchantName!,
                    MerchantWebLink = request.MerchantWebLink!,
                    MerchantReturnUrl = request.MerchantReturnUrl!,
                    MerchantIpnUrl = request.MerchantIpnUrl!,
                    SecretKey = request.SecretKey!,
                    UpdatedBy = request.UpdatedBy
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Update fail!"
                };
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDisease([FromQuery] Guid id)
        {
            MerchantDeleteResponse response = new MerchantDeleteResponse();
            try
            {
                var rs = await _mediator.Send(new DeleteMerchantCommand
                {
                    Id = id,
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Delete fail!"
                };
            }
            return Ok(response);
        }*/
    }
}
