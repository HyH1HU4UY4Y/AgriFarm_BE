using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Service.FarmSite.Commands;
using Service.FarmSite.Commands.Farms;
using Service.FarmSite.DTOs;
using Service.FarmSite.Queries;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmSite.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private IMediator _mediator;

        public SitesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*
        TODO:
            - for anonymous user 
        */

        
        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery]Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null)
        {

            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (id == null && identity == SystemIdentity.Supervisor)
            {
                /*if (identity != SystemIdentity.Supervisor)
                {
                    return NotFound();
                }*/

                PaginationRequest page = new(pageNumber, pageSize);
                var items = await _mediator.Send(new GetAllFarmQuery { 
                    Pagination = page
                });


                return Ok(new DefaultResponse<List<SiteResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetFarmByIdQuery
            {
                Id = identity == SystemIdentity.Supervisor? id.Value: sId
            });


            return Ok(new DefaultResponse<FullSiteResponse>
            {
                Data = item,
                Status = 200
            });
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] SiteCreateRequest request)
        {
            var rs = await _mediator.Send(new CreateNewFarmCommand
            {
                Site = request,
                IsActive = true
            });

            return StatusCode(201);
        }

        [Authorize(Roles=Roles.Admin)]
        [HttpPost("add-position")]
        public async Task<IActionResult> AddPosition(
            [FromQuery][Required]Guid siteId,
            [FromBody] List<PositionPoint> request)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            var rs = await _mediator.Send(new SetPositionCommand
            {
                Positions = request,
                SiteId  = identity == SystemIdentity.Supervisor ? siteId : sId

            });

            return StatusCode(201, new DefaultResponse<FullSiteResponse>
            {
                Data = rs,
                Status = 201
            });
        }


        
        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, [FromBody] SiteEditRequest request)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            var rs = await _mediator.Send(new UpdateFarmCommand
            {
                Id = identity == SystemIdentity.Supervisor ? id : sId,
                Site = request
            });

            return Ok(new DefaultResponse<FullSiteResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteFarmCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
