using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.Queries.Seasons;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Service.FarmCultivation.Commands.Products;

namespace Service.FarmCultivation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {

        private IMediator _mediator;

        public SeasonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            if (id == null)
            {
                var items = await _mediator.Send(new GetSeasonsQuery
                {

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
        public async Task<IActionResult> Post([FromBody] SeasonRequest request)
        {
            var rs = await _mediator.Send(new CreateSeasonCommand
            {
                Season = request
            });


            return StatusCode(201);
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] SeasonRequest request)
        {
            var rs = await _mediator.Send(new UpdateSeasonCommand
            {
                Id = id,
                Season = request
            });

            return NoContent();
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
