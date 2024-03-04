using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmCultivation.Queries.Products;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

using SharedApplication.Pagination;
using Service.FarmCultivation.DTOs.Products;
using Service.FarmCultivation.Commands.Products;
using Asp.Versioning;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace Service.FarmCultivation.Controllers
{
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class HarvestProductsController : ControllerBase
    {
        private IMediator _mediator;

        public HarvestProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery][Required] Guid seasonId,
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

                var items = await _mediator.Send(new GetProductsQuery
                {
                    Pagination = page,
                    SeasonId = seasonId,
                    SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<ProductResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetProductByIdQuery
            {
                Id = id.Value
            });



            return Ok(new DefaultResponse<ProductResponse>
            {
                Data = item,
                Status = 200
            });
        }


        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromQuery][Required] Guid seasonId,
            [FromBody] ProductRequest request,
            [FromQuery] Guid? siteId
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }

            var rs = await _mediator.Send(new CreateProductCommand
            {
                Product = request,
                SeasonId = seasonId,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });


            return StatusCode(201, new DefaultResponse<ProductResponse>
            {
                Data = rs,
                Status = 201
            });
        }

/*
        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] ProductRequest request)
        {
            var rs = await _mediator.Send(new UpdateProductCommand
            {
                Id = id,
                Product = request
            });

            return Ok(new DefaultResponse<ProductResponse>
            {
                Data = rs,
                Status = 200
            });
        }*/

        [HttpPut("harvest")]
        public async Task<IActionResult> Harvest([Required][FromQuery] Guid id, [FromBody] HarvestRequest request)
        {
            var rs = await _mediator.Send(new HarvestProductCommand
            {
                Id = id,
                Harvest = request
            });

            return Ok(
                /*new DefaultResponse<ProductResponse>
                {
                    Data = rs,
                    Status = 200
                }*/
            );
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            await _mediator.Send(new DeleteProductCommand 
            {
                Id = id 
            });

            return NoContent();
        }
    }
}
