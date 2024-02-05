using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Pesticide.Commands.FarmPesticides;
using Service.Pesticide.DTOs;
using Service.Pesticide.Queries;
using Service.Pesticide.Queries.FarmPesticides;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FarmPesticidesController : ControllerBase
    {
        private IMediator _mediator;

        public FarmPesticidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if (id == null)
            {
                var items = await _mediator.Send(new GetFarmPesticidesQuery());

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
        public async Task<IActionResult> Post([FromBody] PesticideRequest request)
        {
            var rs = await _mediator.Send(new AddFarmPesticideCommand { Pesticide = request });

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] PesticideRequest request)
        {

            var rs = await _mediator.Send(new UpdateFarmPesticideCommand
            {
                Id = id,
                Pesticide = request
            });

            return NoContent();
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
