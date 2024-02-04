using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Queries;
using SharedDomain.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("check-valid")]
        public async Task<IActionResult> CheckValidName([FromQuery] string email)
        {
            var rs = await _mediator.Send(new CheckValidAccountQuery { Email = email });
            if (rs)
            {
                return Accepted(new DefaultResponse<bool>
                {
                    Data = true,
                    Message = "Account name is valid.",
                    Status = 202
                    
                });
            }
            else
            {
                return BadRequest(new DefaultResponse<bool>
                {
                    Data = true,
                    Message = "Account name is not valid.",
                    Status = 400
                });
            }
        }

        
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
