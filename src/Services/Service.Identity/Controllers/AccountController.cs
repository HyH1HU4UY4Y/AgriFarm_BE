using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Users;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedApplication.Authorize;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> CheckValidName([FromQuery][EmailAddress] string email)
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

            Guid uId = Guid.Empty;
            Guid sId = Guid.Empty;
            if(!Guid.TryParse(HttpContext.User.GetUserId(), out uId)
                || !Guid.TryParse(HttpContext.User.GetSiteId(), out sId)
                )
            {
                return Unauthorized();
            }

            var user = await _mediator.Send(new GetMemberByIdQuery
            {
                UserId = uId,
                SiteId = sId
            });

            return Ok(new DefaultResponse<UserDetailResponse>
            {
                Data = user,
                Status = 200
            });
        }

        
        [HttpPut("edit-profile")]
        public async Task<IActionResult> Edit([FromBody] SaveMemberDetailRequest request)
        {
            Guid uId = Guid.Empty;
            if (!Guid.TryParse(HttpContext.User.GetUserId(), out uId))
            {
                return Unauthorized();
            }

            var rs = await _mediator.Send(new UpdateMemberCommand
            {
                User = request,
                Id = uId,
            });

            return NoContent();

        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]string value)
        {


            return NoContent();
        }

        
    }
}
