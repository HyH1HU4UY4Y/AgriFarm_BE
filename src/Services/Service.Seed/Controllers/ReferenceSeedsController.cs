using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Seed.Commands;
using Service.Seed.Commands.RefSeeds;
using Service.Seed.DTOs;
using Service.Seed.Queries;
using Service.Seed.Queries.RefSeeds;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Seed.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ReferenceSeedsController : ControllerBase
    {
        private IMediator _mediator;

        public ReferenceSeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if (id == null)
            {
                var items = await _mediator.Send(new GetRefSeedsQuery());

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

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] RefSeedRequest request)
        {

            var rs = await _mediator.Send(new UpdateRefSeedCommand
            {
                Id = id,
                Seed = request
            });

            return NoContent();
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
