using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedDomain.Defaults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class StaffsController : ControllerBase
    {
        private IMediator _mediator;

        public StaffsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<StaffsController>
        [HttpGet("get")]
        public async Task<IActionResult> GetStaff([FromQuery]Guid siteId, [FromQuery]Guid? userId = null)
        {
            if (userId == null)
            {
                var users = await _mediator.Send(new GetStaffsQuery { SiteId = siteId });

                return Ok(users);
            }

            var user = await _mediator.Send(new GetMemberByIdQuery 
            { 
                SiteId = siteId, 
                UserId = (Guid)userId 
            });

            return Ok(user);
            
        }

        
        [HttpGet("value")]
        public string GetValue()
        {
            return "value";
        }

        // POST api/<StaffsController>
        [HttpPost("add-new-staff")]
        public async Task<IActionResult> AddNewStaff([FromQuery]Guid siteId, [FromBody] AddStaffRequest request)
        {
            var rs = await _mediator.Send(new CreateMemberCommand
            {
                SiteId = siteId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName,
                AccountType = AccountType.Member
            });

            return Ok(rs);
        }

        // PUT api/<StaffsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StaffsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
