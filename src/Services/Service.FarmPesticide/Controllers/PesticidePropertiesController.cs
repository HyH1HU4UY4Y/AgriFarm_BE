using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Pesticide.Commands.FarmPesticides;
using Service.Pesticide.DTOs;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.Controllers
{
    [Route("api/v{version:apiVersion}/pesticide-props")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class PesticidePropertiesController : ControllerBase
    {
        private IMediator _mediator;

        public PesticidePropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid pesticideId,
            [FromBody][MaxLength(10), MinLength(1)] List<PropertyValue> request)
        {

            var rs = await _mediator.Send(new UpdatePropertyCommand
            {
                PesticideId = pesticideId,
                Properties = request
            });

            return Ok(new DefaultResponse<PesticideResponse>
            {
                Status = 200,
                Data = rs
            });
        }
    }
}
