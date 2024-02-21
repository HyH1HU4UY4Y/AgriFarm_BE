using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Seed.Commands.FarmSeeds;
using Service.Seed.DTOs;
using Service.Seed.Queries.FarmSeeds;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Seed.Controllers
{
    [Route("api/v{version:apiVersion}/farm-seeds")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class FarmSeedsController : ControllerBase
    {
        private IMediator _mediator;

        public FarmSeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null,
            [FromQuery]Guid? siteId = null,
            [FromHeader]int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if(id == null)
            {
                if(identity == SystemIdentity.Supervisor && siteId == null) { 
                    return NotFound();
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetFarmSeedsQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor? siteId.Value: sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<SeedResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetFarmSeedByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<SeedResponse>
            {
                Data = item,
                Status = 200
            });
            
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] SeedCreateRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new AddFarmSeedCommand { 
                Seed = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<SeedResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery]Guid id, 
            [FromBody] SeedInfoRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmSeedCommand
            {
                Id = id,
                Seed = request
            });

            return Ok(new DefaultResponse<SeedResponse>
            {
                Status = 200,
                Data = rs
            });
        }
        

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {
            var rs = await _mediator.Send(new RemoveFarmSeedCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
