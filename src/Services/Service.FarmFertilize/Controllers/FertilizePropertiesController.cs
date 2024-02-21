using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Fertilize.Commands.FarmFertilizes;
using Service.Fertilize.DTOs;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.Controllers
{
    [Route("api/v{version:apiVersion}/fertilize-props")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizePropertiesController : ControllerBase
    {
        private IMediator _mediator;

        public FertilizePropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid fertilizeId,
            [FromBody][MaxLength(10), MinLength(1)] List<PropertyValue> request)
        {

            var rs = await _mediator.Send(new UpdatePropertyCommand
            {
                FertilizeId = fertilizeId,
                Properties = request
            });

            return Ok(new DefaultResponse<FertilizeResponse>
            {
                Status = 200,
                Data = rs
            });
        }
    }
}
