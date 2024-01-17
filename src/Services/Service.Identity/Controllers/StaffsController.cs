using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands;
using Service.Identity.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private IMediator _mediator;

        public StaffsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<StaffsController>
        [HttpGet]
        public async Task<IActionResult> GetStaff(Guid siteId)
        {
            var rs = await _mediator.Send(new GetStaffsQuery { SiteId = siteId });

            return Ok(rs);
        }

        // GET api/<StaffsController>/5
        [HttpGet()]
        public string Get()
        {
            return "value";
        }

        // POST api/<StaffsController>
        [HttpPost("add-new-staff")]
        public async Task<IActionResult> AddNewStaff([FromBody] CreateMemberCommand request)
        {
            var rs = await _mediator.Send(request);

            return Ok();
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
