using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.FarmScheduling.DTOs.Details;
using Service.FarmScheduling.Queries.Activities;
using SharedApplication.Authorize;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/v{version:apiVersion}/use")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class UsingsController : ControllerBase
    {
        private IMediator _mediator;

        public UsingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([Required][FromQuery]Guid activityId)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            var rs = await _mediator.Send(new GetUsingDetailQuery
            {
                ActivityId = activityId,
                UserId = uId
            });

            return Ok(new DefaultResponse<UsingDetailResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        
    }
}
