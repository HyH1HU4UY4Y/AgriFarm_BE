using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Service.Water.DTOs;
using Service.Water.Queries;
using Service.Water.Commands;

namespace Service.Water.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterRecordController : ControllerBase
    {
        private IMediator _mediator;

        public WaterRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if (id == null)
            {
                var items = await _mediator.Send(new GetFarmWatersQuery());

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
        public async Task<IActionResult> Post([FromBody] WaterRequest request)
        {
            var rs = await _mediator.Send(new AddFarmWaterCommand { Water = request });

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] WaterRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmWaterCommand
            {
                Id = id,
                Water = request
            });

            return NoContent();
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
