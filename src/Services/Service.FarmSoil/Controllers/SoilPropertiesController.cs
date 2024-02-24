using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Soil.Command;
using Service.Soil.DTOs;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Soil.Controllers
{
    [Route("api/v{version:apiVersion}/soil-properties")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SoilPropertiesController : ControllerBase
    {
        private IMediator _mediator;

        public SoilPropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("add-property")]
        public async Task<IActionResult> AddProperty(
            [FromQuery][Required] Guid landId,
            [FromBody] List<PositionPoint> request,
            [FromQuery] Guid? siteId = null)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                throw new BadRequestException($"{"siteId"} is required!");
            }

            var rs = await _mediator.Send(new SetPositionCommand
            {
                Positions = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId,
                LandId = landId

            });

            return StatusCode(201, new DefaultResponse<LandResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
