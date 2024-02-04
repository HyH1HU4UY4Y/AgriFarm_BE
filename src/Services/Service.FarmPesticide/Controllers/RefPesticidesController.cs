using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Pesticide.Commands.RefPesticides;
using Service.Pesticide.DTOs;
using Service.Pesticide.Queries.RefPesticides;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Pesticide.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RefPesticidesController : ControllerBase
    {
        private IMediator _mediator;

        public RefPesticidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if (id == null)
            {
                var items = await _mediator.Send(new GetRefPesticidesQuery());

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<RefPesticideResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetRefPesticideByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<RefPesticideResponse>
            {
                Data = item,
                Status = 200
            });

        }



        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] RefPesticideRequest request)
        {
            var rs = await _mediator.Send(new AddRefPesticideCommand { Pesticide = request });

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] RefPesticideRequest request)
        {

            var rs = await _mediator.Send(new UpdateRefPesticideCommand
            {
                Id = id,
                Pesticide = request
            });

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveRefPesticideCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
