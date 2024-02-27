using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Pesticide.Commands.FarmPesticides;
using Service.Pesticide.DTOs;
using Service.Pesticide.Queries;
using Service.Pesticide.Queries.FarmPesticides;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.Controllers
{
    [Route("api/v{version:apiVersion}/farm-pesticides")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class FarmPesticidesController : ControllerBase
    {
        private IMediator _mediator;

        public FarmPesticidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? id = null,
            [FromQuery] Guid? siteId = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (id == null)
            {
                if (identity == SystemIdentity.Supervisor && siteId == null)
                {
                    return NotFound();
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetFarmPesticidesQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<PesticideResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetFarmPesticideByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<PesticideResponse>
            {
                Data = item,
                Status = 200
            });

        }

        
        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] PesticideCreateRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new AddFarmPesticideCommand { 
                Pesticide = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<PesticideResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPost("supply")]
        public async Task<IActionResult> Supply(
            [FromQuery][Required] Guid id,
            [FromBody] SupplyRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new SupplyPesticideCommand
            {
                Id = id,
                Details = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(200, new DefaultResponse<PesticideResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, 
            [FromBody] PesticideInfoRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmPesticideCommand
            {
                Id = id,
                Pesticide = request
            });

            return Ok(new DefaultResponse<PesticideResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveFarmPesticideCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
