
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Equipment.Commands;
using Service.Equipment.DTOs;
using Service.Equipment.Queries;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Equipment.Controllers
{
    [Route("api/v{version:apiVersion}/farm-equipments")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class EquipmentsController : ControllerBase
    {
        private IMediator _mediator;

        public EquipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? id = null,
            [FromQuery] Guid? siteId = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {

            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (id == null)
            {
                if (identity == SystemIdentity.Supervisor && siteId == null)
                {
                    return NotFound();
                }

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetEquipmentsQuery
                {
                    Pagination = page,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);
                
                return Ok(new DefaultResponse<List<EquipmentResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetEquipmentByIdQuery
            {
                Id = id.Value
            });

            return Ok(new DefaultResponse<EquipmentResponse>
            {
                Data = item,
                Status = 200
            });
        }



        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] EquipmentRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return NotFound();
            }

            var rs = await _mediator.Send(new AddEquipmentCommand
            {
                Equipment = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(201, new DefaultResponse<EquipmentResponse>
            {
                Data = rs,
                Status = 201
            });
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, [FromBody] EquipmentRequest request)
        {

            var rs = await _mediator.Send(new UpdateEquipmentCommand
            {
                Id = id,
                Equipment = request
            });

            return Ok(new DefaultResponse<EquipmentResponse>
            {
                Data = rs,
                Status = 200
            });
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid id)
        {
            await _mediator.Send(new RemoveEquipmentCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
