using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.Queries.Activities;
using SharedApplication.Authorize.Values;
using SharedApplication.Authorize;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using Service.FarmScheduling.Commands.Activities;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/v{version:apiVersion}/time-table")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TimeTableController : ControllerBase
    {
        private IMediator _mediator;

        public TimeTableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            /*if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }*/

            PaginationRequest page = new(pageNumber, pageSize);

            var items = await _mediator.Send(new GetOwnScheduleQuery
            {
                Pagination = page,
                UserId = uId
            });

            Response.AddPaginationHeader(items.MetaData);

            return Ok(new DefaultResponse<List<ActivityResponse>>
            {
                Data = items,
                Status = 200
            });

        }


        
        [HttpPost("done")]
        public async Task<IActionResult> Done([FromQuery][Required] Guid activityId)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor) return StatusCode(404);

            var rs = await _mediator.Send(new MarkDoneCommand
            {
                Id = activityId,
                UserId = uId
            });

            return Ok();
        }


    }
}
