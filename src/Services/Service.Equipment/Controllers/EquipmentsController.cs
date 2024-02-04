
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Equipment.Commands;
using Service.Equipment.DTOs;
using Service.Equipment.Queries;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Equipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private IMediator _mediator;

        public EquipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]Guid? id = null)
        {
            if (id == null)
            {
                var items = await _mediator.Send(new GetEquipmentsQuery
                {

                });

                Response.AddPaginationHeader(items.MetaData);
                
                return Ok(new DefaultResponse<List<EquipmentResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetEquipmentByIdQuery
            {
                Id = id.Value
            });

            return Ok(new DefaultResponse<EquipmentResponse>
            {
                Data = item,
                Status = 200
            });
        }



        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] EquipmentRequest request)
        {
            var rs = await _mediator.Send(new AddEquipmentCommand
            {
                Equipment = request
            });
            return StatusCode(201, new DefaultResponse<Guid>
            {
                Data = rs,
                Status = 201
            });
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, [FromBody] EquipmentRequest request)
        {

            var rs = await _mediator.Send(new UpdateEquipmentCommand
            {
                Id = id,
                Equipment = request
            });

            return NoContent();
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid id)
        {
            await _mediator.Send(new RemoveEquipmentCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
