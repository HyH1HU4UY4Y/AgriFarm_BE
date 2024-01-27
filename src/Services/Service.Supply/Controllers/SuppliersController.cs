using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Supply.Commands;
using Service.Supply.DTOs;
using Service.Supply.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Supply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public SuppliersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]Guid? id = null)
        {
            if(id == null)
            {
                var items = await _mediator.Send(new GetAllSupplierQuery());
                return Ok(items);
            }

            var item = await _mediator.Send(new GetSupplierByIdQuery {Id = (Guid)id });

            return Ok(item);
        }


        
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] SupplierRequest request)
        {
            var cmd = _mapper.Map<CreateNewSupplierCommand>(request);
            var rs = await _mediator.Send(cmd);

            return CreatedAtAction(nameof(Get),rs, null);
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery]Guid id, [FromBody] SupplierRequest request)
        {

            var cmd = _mapper.Map<UpdateSupplierCommand>(request);
            cmd.Id = id;

            await _mediator.Send(cmd);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteSupplierCommand { Id = id });

            return NoContent();
        }
    }
}
