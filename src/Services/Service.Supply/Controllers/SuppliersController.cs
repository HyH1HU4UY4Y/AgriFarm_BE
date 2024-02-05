using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Supply.Commands.Suppliers;
using Service.Supply.DTOs;
using Service.Supply.Queries.Suppliers;
using SharedDomain.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Supply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]Guid? id = null)
        {
            if(id == null)
            {
                var items = await _mediator.Send(new GetAllSupplierQuery());
                return Ok(items);
            }

            var item = await _mediator.Send(new GetSupplierByIdQuery {
                Id = id.Value 
            });

            return Ok(item);
        }


        
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] SupplierRequest request)
        {
            
            var rs = await _mediator.Send(new CreateNewSupplierCommand
            {
                Supplier = request
            });

            return StatusCode(201, new DefaultResponse<Guid>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery]Guid id, [FromBody] SupplierRequest request)
        {


            await _mediator.Send(new UpdateSupplierCommand
            {
                Id = id,
                Supplier = request
            });

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteSupplierCommand { 
                Id = id 
            });

            return NoContent();
        }
    }
}
