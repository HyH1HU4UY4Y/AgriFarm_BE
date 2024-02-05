using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmScheduling.Commands.Tags;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            if (id == null)
            {

            }

            return Ok();
        }


        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            var rs = await _mediator.Send(new CreateTagCommand
            {
                Tag = name
            });


            return StatusCode(201, new DefaultResponse<Guid>
            {
                Data = rs,
                Status = 201
            });
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {

            await _mediator.Send(new DeleteTagCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
