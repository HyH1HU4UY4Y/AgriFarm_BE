using Infrastructure.Payment.VnPay.Config;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Payment.Commands;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentDTOs;
using Service.Payment.Queries.MerchantQueries;

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

        //[Authorize(Roles = Roles.Admin)]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            // Get by id
            MerchantDetailResponse response = new MerchantDetailResponse();
            var rs = await _mediator.Send(new GetMerchantByQuery
            {
                MerchantId = id
            });
            if (rs != null)
            {
                response.data = rs;
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
