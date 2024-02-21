using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Pesticide.Commands.RefPesticides;
using Service.Pesticide.DTOs;
using Service.Pesticide.Queries.RefPesticides;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Pesticide.Controllers
{
    [Route("api/v{version:apiVersion}/ref-pesticides")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class RefPesticidesController : ControllerBase
    {
        private IMediator _mediator;

        public RefPesticidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {

            if (id == null)
            {
                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetRefPesticidesQuery
                {
                    Pagination = page
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<RefPesticideResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetRefPesticideByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<RefPesticideResponse>
            {
                Data = item,
                Status = 200
            });

        }


        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] RefPesticideRequest request)
        {
            var rs = await _mediator.Send(new AddRefPesticideCommand { Pesticide = request });

            return StatusCode(201, new DefaultResponse<RefPesticideResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] RefPesticideRequest request)
        {

            var rs = await _mediator.Send(new UpdateRefPesticideCommand
            {
                Id = id,
                Pesticide = request
            });

            return Ok(new DefaultResponse<RefPesticideResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveRefPesticideCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
