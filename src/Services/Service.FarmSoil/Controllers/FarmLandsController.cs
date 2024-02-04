
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Soil.Queries;
using SharedApplication.Pagination;
using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Soil.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FarmLandsController : ControllerBase
    {
        private IMediator _mediator;

        public FarmLandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<FarmLandsController>
        [HttpGet]
        public async Task<IActionResult> Get([Required][FromQuery]string siteCode)
        {
            var rs = await _mediator.Send(new GetLandBySiteCodeQuery
            {
                SiteCode = siteCode
            });

            Response.AddPaginationHeader(rs.MetaData);

            return Ok(rs);
        }

        // GET api/<FarmLandsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FarmLandsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FarmLandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FarmLandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
