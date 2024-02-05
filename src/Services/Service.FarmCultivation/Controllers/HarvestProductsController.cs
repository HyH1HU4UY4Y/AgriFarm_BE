using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmCultivation.Queries.Products;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Service.FarmCultivation.DTOs.Products;
using Service.FarmCultivation.Commands.Products;

namespace Service.FarmCultivation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestProductsController : ControllerBase
    {
        private IMediator _mediator;

        public HarvestProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            if (id == null)
            {
                var items = await _mediator.Send(new GetHarvestProductsQuery
                {

                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<HarvestProductResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetHarvestProductByIdQuery
            {
                Id = id.Value
            });



            return Ok(new DefaultResponse<HarvestProductResponse>
            {
                Data = item,
                Status = 200
            });
        }


        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] HarvestProductRequest request)
        {
            var rs = await _mediator.Send(new CreateHarvestProductCommand
            {
                HarvestProduct = request
            });


            return StatusCode(201);
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] HarvestProductRequest request)
        {
            var rs = await _mediator.Send(new UpdateHarvestProductCommand
            {
                Id = id,
                HarvestProduct = request
            });

            return NoContent();
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            await _mediator.Send(new DeleteHarvestProductCommand 
            {
                Id = id 
            });

            return NoContent();
        }
    }
}
