using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.Queries.Seasons;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Asp.Versioning;
using Service.FarmCultivation.DTOs.Seasons;
using Microsoft.AspNetCore.Authorization;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;

namespace Service.FarmCultivation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class SeasonsController : ControllerBase
    {

        private IMediator _mediator;

        public SeasonsController(IMediator mediator)
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
                    return StatusCode(404);
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetSeasonsQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<SeasonDetailResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetSeasonByIdQuery
            {
                Id = id.Value
            });



            return Ok(new DefaultResponse<SeasonDetailResponse>
            {
                Data = item,
                Status = 200
            });
        }


        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] SeasonCreateRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }

            var rs = await _mediator.Send(new CreateSeasonCommand
            {
                Season = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });


            return StatusCode(201, new DefaultResponse<SeasonDetailResponse>
            {
                Data = rs,
                Status = 201
            });
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] SeasonEditRequest request)
        {
            var rs = await _mediator.Send(new UpdateSeasonCommand
            {
                Id = id,
                Season = request
            });

            return Ok(new DefaultResponse<SeasonDetailResponse>
            {
                Data = rs,
                Status = 200
            });
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            await _mediator.Send(new DeleteSeasonCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
