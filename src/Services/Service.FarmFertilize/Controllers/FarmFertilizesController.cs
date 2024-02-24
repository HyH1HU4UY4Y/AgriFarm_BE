using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Fertilize.Commands.FarmFertilizes;
using Service.Fertilize.DTOs;
using Service.Fertilize.Queries.FarmFertilizes;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.Controllers
{
    [Route("api/v{version:apiVersion}/farm-fertilizes")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class FarmFertilizesController : ControllerBase
    {
        private IMediator _mediator;

        public FarmFertilizesController(IMediator mediator)
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

                var items = await _mediator.Send(new GetFarmFertilizesQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<FertilizeResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetFarmFertilizeByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<FertilizeResponse>
            {
                Data = item,
                Status = 200
            });

        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] FertilizeCreateRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new AddFarmFertilizeCommand { 
                Fertilize = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<FertilizeResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, 
            [FromBody] FertilizeInfoRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmFertilizeCommand
            {
                Id = id,
                Fertilize = request
            });

            return Ok(new DefaultResponse<FertilizeResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveFarmFertilizeCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
