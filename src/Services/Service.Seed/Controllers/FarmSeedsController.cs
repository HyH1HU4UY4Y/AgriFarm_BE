using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Seed.Commands.FarmSeeds;
using Service.Seed.DTOs;
using Service.Seed.Queries;
using Service.Seed.Queries.FarmSeeds;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Seed.Controllers
{
    [Route("api/v{version:apiVersion}/farm-seeds")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FarmSeedsController : ControllerBase
    {
        private IMediator _mediator;

        public FarmSeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if(id == null)
            {
                var items = await _mediator.Send(new GetFarmSeedsQuery());

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
        public async Task<IActionResult> Post([FromBody] SeedRequest request)
        {
            var rs = await _mediator.Send(new AddFarmSeedCommand { Seed = request});

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery]Guid id, [FromBody] SeedRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmSeedCommand
            {
                Id = id,
                Seed = request
            });

            return NoContent();
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
