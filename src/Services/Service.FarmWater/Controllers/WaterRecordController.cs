using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Service.Water.DTOs;
using Service.Water.Queries;
using Service.Water.Commands;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace Service.Water.Controllers
{
    [Route("api/v{version:apiVersion}/farm-water")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class WaterRecordController : ControllerBase
    {
        private IMediator _mediator;

        public WaterRecordController(IMediator mediator)
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

                var items = await _mediator.Send(new GetFarmWatersQuery { 
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<WaterResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetFarmWaterByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<WaterResponse>
            {
                Data = item,
                Status = 200
            });

        }



        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] WaterRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new AddFarmWaterCommand { 
                Water = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<WaterResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] WaterRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmWaterCommand
            {
                Id = id,
                Water = request
            });

            return Ok(new DefaultResponse<WaterResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveFarmWaterCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
