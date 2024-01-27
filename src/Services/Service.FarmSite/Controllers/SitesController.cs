using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.FarmSite.Commands;
using Service.FarmSite.Queries;
using SharedDomain.Defaults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmSite.Controllers
{
    [Authorize(Roles = Roles.SuperAdmin)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private IMediator _mediator;

        public SitesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: api/<SitesController>
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var rs = await _mediator.Send(new GetAllSiteQuery());

            return Ok(rs);
        }

        

        // POST api/<SitesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNewFarmCommand request)
        {
            var rs = await _mediator.Send(request);

            return StatusCode(201);
        }

        // PUT api/<SitesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SitesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
