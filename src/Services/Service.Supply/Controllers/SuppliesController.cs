using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Supply.Commands.Supplies;
using Service.Supply.DTOs;
using Service.Supply.Queries.Supplies;
using SharedApplication.Authorize;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Supply.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class SuppliesController : ControllerBase
    {
        private IMediator _mediator;

        public SuppliesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by")]
        public async Task<IActionResult> Get(
            [Required][FromQuery]Guid itemId,
            [FromQuery] Guid? siteId = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            PaginationRequest page = new(pageNumber, pageSize);

            var items = await _mediator.Send(new GetItemSupplyDetailsQuery
            {
                ItemId = itemId,
                Pagination = page
            });

            Response.AddPaginationHeader(items.MetaData);

            return Ok(new DefaultResponse<PagedList<SupplyDetailResponse>>
            {
                Data = items,
                Status = 200
            });
        }


        
        /*[HttpPost("add-contract")]
        public async Task<IActionResult> Post([FromBody] NewSupplyRequest request)
        {
            var cmd = new CreateSupplyDetailCommand {
                
            };
            var rs = await _mediator.Send(cmd);

            return StatusCode(201, rs);
        }*/

        
        /*[HttpPut("put")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid id)
        {
            await _mediator.Send(new DeleteSupplyContractCommand
            {
                Id = id 
            });

            return NoContent();
        }
    }
}
