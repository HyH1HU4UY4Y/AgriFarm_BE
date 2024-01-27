using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Auth;
using Service.Identity.DTOs;
using SharedDomain.Common;

namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
            return Ok(new DefaultResponse<AuthorizeResponse>
            {
                Data = rs,
                Status = 200
            });
        }
    }
}
