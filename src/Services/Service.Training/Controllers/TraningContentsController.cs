using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Training.Commands.TraningContents;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingContents;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Training.Controllers
{
    [Route("api/v{version:apiVersion}/contents")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class TrainingContentsController : ControllerBase
    {
        private IMediator _mediator;

        public TrainingContentsController(IMediator mediator)
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

                var items = await _mediator.Send(new GetTrainingContentsQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<FullContentResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetTrainingContentByIdQuery {
                Id = id.Value,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });


            return Ok(new DefaultResponse<FullContentResponse>
            {
                Data = item,
                Status = 200
            });

        }



        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] ContentRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }
            var rs = await _mediator.Send(new AddTrainingContentCommand { 
                TrainingContent = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] ContentRequest request)
        {

            var rs = await _mediator.Send(new UpdateTrainingContentCommand
            {
                Id = id,
                TrainingContent = request
            });

            return Ok(new DefaultResponse<FullContentResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveTrainingContentCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
