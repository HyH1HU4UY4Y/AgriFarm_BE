using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.FarmScheduling.Commands.Activities;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.Queries.Activities;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ActivitiesController : ControllerBase
    {
        private IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery][Required] Guid seasonId,
            [FromQuery] Guid? id = null,
            [FromQuery] Guid? siteId = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            
            if (id == null)
            {
                if (identity == SystemIdentity.Supervisor && siteId == null)
                {
                    return StatusCode(404);
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetActivitiesQuery
                {
                    Pagination = page,
                    SeasonId = seasonId,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<ActivityResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            return Ok();
        }


        
        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] ActivityCreateRequest request)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            var rs = await _mediator.Send(new CreateActivityCommand
            {
                Activity = request,
                SiteId = sId,
                
            });

            return StatusCode(201, new DefaultResponse<ActivityResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put(
            [Required][FromQuery]Guid id, 
            [FromBody] ActivityCreateRequest request)
        {

            return NoContent();
        }

        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteActivityCommand
            {
                Id = id
            });
            return NoContent();
        }
    }
}
