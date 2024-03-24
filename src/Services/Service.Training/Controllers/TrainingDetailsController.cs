using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Training.Commands.TrainingDetails;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingDetails;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Training.Controllers
{
    [Route("api/v{version:apiVersion}/details")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TrainingDetailsController : ControllerBase
    {
        private IMediator _mediator;

        public TrainingDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
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

                var items = await _mediator.Send(new GetTrainingDetailsQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<DetailResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetTrainingDetailByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<DetailResponse>
            {
                Data = item,
                Status = 200
            });

        }

        [HttpGet("get-by-activity")]
        public async Task<IActionResult> GetByActivity(
            [FromQuery][Required] Guid activityId
            )
        {
            
            var item = await _mediator.Send(new GetTrainingDetailByActivityQuery { 
                ActivityId = activityId 
            });


            return Ok(new DefaultResponse<DetailResponse>
            {
                Data = item,
                Status = 200
            });

        }




/*
        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] DetailRequest request)
        {

            var rs = await _mediator.Send(new UpdateTrainingDetailCommand
            {
                Id = id,
                TrainingDetail = request
            });

            return NoContent();
        }
*/
    }

}
