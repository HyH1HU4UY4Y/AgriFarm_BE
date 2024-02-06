using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Service.FarmSite.Commands;
using Service.FarmSite.Commands.Farms;
using Service.FarmSite.DTOs;
using Service.FarmSite.Queries;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmSite.Controllers
{
    [Authorize(Roles = Roles.SuperAdmin)]
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

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery]Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null)
        {
            if(id == null)
            {
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

            var item = await _mediator.Send(new GetFarmByIdQuery());


            return Ok(new DefaultResponse<SiteResponse>
            {
                Data = item,
                Status = 200
            });
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteRequest request)
        {
            var rs = await _mediator.Send(new CreateNewFarmCommand
            {
                Site = request,
                IsActive = true
            });

            return StatusCode(201);
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, [FromBody] SiteEditRequest request)
        {
            var rs = await _mediator.Send(new UpdateFarmCommand
            {
                Id = id,
                Site = request
            });

            return NoContent();
        }


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
