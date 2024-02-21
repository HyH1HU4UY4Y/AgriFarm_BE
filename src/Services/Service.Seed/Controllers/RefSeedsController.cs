using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Seed.Commands;
using Service.Seed.Commands.RefSeeds;
using Service.Seed.DTOs;
using Service.Seed.Queries;
using Service.Seed.Queries.RefSeeds;
using SharedApplication.Authorize;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Seed.Controllers
{
    [Route("api/v{version:apiVersion}/ref-seeds")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class RefSeedsController : ControllerBase
    {
        private IMediator _mediator;

        public RefSeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (id == null)
            {
                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetRefSeedsQuery
                {
                    Pagination = page
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<RefSeedResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetRefSeedByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<RefSeedResponse>
            {
                Data = item,
                Status = 200
            });

        }



        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] RefSeedRequest request)
        {
            var rs = await _mediator.Send(new AddRefSeedCommand { Seed = request });

            return StatusCode(201, new DefaultResponse<RefSeedResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] RefSeedRequest request)
        {

            var rs = await _mediator.Send(new UpdateRefSeedCommand
            {
                Id = id,
                Seed = request
            });

            return Ok(new DefaultResponse<RefSeedResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveRefSeedCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
