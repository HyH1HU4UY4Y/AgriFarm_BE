﻿using Infrastructure.Payment.Utils;
using Infrastructure.Payment.VnPay.Config;
using Infrastructure.Payment.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Payment.Commands;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentDTOs;
using Service.Payment.Queries.MerchantQueries;
using Service.Payment.Queries.Payment;
using System;
using Mapster;

namespace Service.Payment.Controllers
{
    [Route("api/payment/")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly VnpayConfig _vnpayConfig;

        public PaymentsController(IMediator mediator, 
            IOptions<VnpayConfig> vnpayConfigOptions)
        {
            this._mediator = mediator;
            this._vnpayConfig = vnpayConfigOptions.Value;
        }

        /*//[Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] MerchantRequest request)
        {
            MerchantResponse response = new MerchantResponse();
            // Get all data
            var rsAll = await _mediator.Send(new GetMerchantQuery
            {
                keyword = request.keyword,
                searchDateFrom = request.searchDateFrom,
                searchDateTo = request.searchDateTo
            });
            // Search, pagination
            if (rsAll.Count() > 0)
            {
                if (request.perPage == 0)
                {
                    request.perPage = (int)PaginationDefault.perPage;
                }
                if (request.pageId == 0)
                {
                    request.pageId = (int)PaginationDefault.pageId;
                }
                var rsSearch = await _mediator.Send(new GetMerchantQuery
                {
                    keyword = request.keyword,
                    searchDateFrom = request.searchDateFrom,
                    searchDateTo = request.searchDateTo,
                    perPage = request.perPage,
                    pageId = request.pageId,
                    getAllDataFlag = false
                });
                response.data = rsSearch;
                response.Pagination = new Pagination
                {
                    totalRecord = rsAll.Count(),
                    perPage = request.perPage,
                    pageId = request.pageId
                };
                response.statusCode = Ok().StatusCode;
            }
            if (response.Pagination.totalRecord == 0)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string> {
                    "Data not found!"
                };
            }
            return Ok(response);
        }

        */

        //[Authorize(Roles = Roles.Admin)]
        /* [HttpGet("get-by-id")]
         [Route("vnpay-return")]
         public async Task<IActionResult> VnpayReturn([FromQuery] Guid id)
         {
             PaymentDetailResponse response = new PaymentDetailResponse();
             var rs = await _mediator.Send(new GetPaymentByQuery
             {
                 PaymentId = id
             });
             if (rs != null)
             {
                 response.Id = rs.Id;
                 response.PaymentStatus = rs.PaymentStatus;
                 response.PaymentMessage = rs.PaymentLastMessage;
                 response.PaymentDate = rs.PaymentDate;
                 response.PaymentRefId = rs.PaymentRefId;
                 response.Amount = rs.PaidAmount;
                 response.Signature = rs.Signature;
                 response.statusCode = Ok().StatusCode;
             }
             else
             {
                 response.statusCode = NoContent().StatusCode;
                 response.message = new List<string> {
                     "Data not found!"
                 };
             }
             return Ok(response);
         }*/

        [HttpGet]
        [Route("vnpay-return")]
        public async Task<IActionResult> VnpayReturn([FromQuery] VnpayPayResponse response)
        {
            string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDTO();
            var processResult = await _mediator.Send(response.Adapt<ProcessVnpayPaymentReturn>());

            if (processResult != (null,null))
            {
                returnModel = processResult.Item1 as PaymentReturnDTO;
                returnUrl = processResult.Item2 as string;
            }

            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect("https://localhost:7218/api/Merchants/get"); 
                /*Redirect($"{returnUrl}?{returnModel.ToQueryString()}"); NoContent();*/
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