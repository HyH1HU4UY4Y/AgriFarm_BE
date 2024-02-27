using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Supply.Commands.Suppliers;
using Service.Supply.DTOs;
using Service.Supply.Queries.Suppliers;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
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
    public class SuppliersController : ControllerBase
    {
        private IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery]Guid? id = null,
            [FromQuery] Guid? siteId = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {

            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (id == null)
            {
                if (identity == SystemIdentity.Supervisor && siteId == null)
                {
                    return StatusCode(404);
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetAllSupplierQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<SupplierResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetSupplierByIdQuery {
                Id = id.Value 
            });

            return Ok(new DefaultResponse<SupplierInfoResponse>
            {
                Data = item,
                Status = 200
            });
        }


        
        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] SupplierRequest request,
            [FromQuery] Guid? siteId = null
            )
        {

            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }

            var rs = await _mediator.Send(new SaveSupplierCommand
            {
                Supplier = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<SupplierInfoResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, [FromBody] SupplierRequest request)
        {


            var rs = await _mediator.Send(new UpdateSupplierCommand
            {
                Id = id,
                Supplier = request
            });

            return Ok(new DefaultResponse<SupplierInfoResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery][Required]Guid id)
        {
            await _mediator.Send(new DeleteSupplierCommand { 
                Id = id 
            });

            return NoContent();
        }
    }
}
