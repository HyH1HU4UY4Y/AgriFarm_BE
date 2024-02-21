using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Seed.Commands.FarmSeeds;
using Service.Seed.DTOs;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Seed.Controllers
{
    [Route("api/v{version:apiVersion}/seed-props")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class SeedPropertiesController : ControllerBase
    {
        private IMediator _mediator;

        public SeedPropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid seedId,
            [FromBody][MaxLength(10), MinLength(1)] List<PropertyValue> request)
        {

            var rs = await _mediator.Send(new UpdatePropertyCommand
            {
                Id = seedId,
                Properties = request
            });

            return Ok(new DefaultResponse<SeedResponse>
            {
                Status = 200,
                Data = rs
            });
        }
    }
}
