using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmSite.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private IMediator _mediator;

        public SitesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<SitesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SitesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
