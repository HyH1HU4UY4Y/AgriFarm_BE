using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Auth;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] TokenCommand command)
        {
            var rs = await _mediator.Send(command);
            if(rs == null || !rs.IsSuccess) return Unauthorized();
            return Ok(rs);
        }
    }
}
