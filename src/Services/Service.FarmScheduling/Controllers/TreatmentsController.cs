using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.FarmScheduling.DTOs.Details;
using Service.FarmScheduling.Queries.Activities;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/v{version:apiVersion}/treatment")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TreatmentsController : ControllerBase
    {
        private IMediator _mediator;

        public TreatmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([Required][FromQuery] Guid activityId)
        {

            var rs = await _mediator.Send(new GetTreatmentDetailQuery
            {
                ActivityId = activityId
            });

            return Ok(new DefaultResponse<TreatmentDetailResponse>
            {
                Data = rs,
                Status = 200
            });
        }

    }
}
