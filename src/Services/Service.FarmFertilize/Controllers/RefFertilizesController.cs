using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Fertilize.Commands.RefFertilizes;
using Service.Fertilize.DTOs;
using Service.Fertilize.Queries.RefFertilizes;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.Controllers
{
    [Route("api/v{version:apiVersion}/ref-fertilizes")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class RefFertilizesController : ControllerBase
    {
        private IMediator _mediator;

        public RefFertilizesController(IMediator mediator)
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

                var items = await _mediator.Send(new GetRefFertilizesQuery());

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<RefFertilizeResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetRefFertilizeByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<RefFertilizeResponse>
            {
                Data = item,
                Status = 200
            });

        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] RefFertilizeRequest request)
        {
            var rs = await _mediator.Send(new AddRefFertilizeCommand { Fertilize = request });

            return StatusCode(201, new DefaultResponse<RefFertilizeResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] RefFertilizeRequest request)
        {

            var rs = await _mediator.Send(new UpdateRefFertilizeCommand
            {
                Id = id,
                Fertilize = request
            });

            return Ok(new DefaultResponse<RefFertilizeResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveRefFertilizeCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
