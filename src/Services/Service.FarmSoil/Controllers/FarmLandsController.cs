
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Service.Soil.Command;
using Service.Soil.DTOs;
using Service.Soil.Queries;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using SharedDomain.Defaults.Measures;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Soil.Controllers
{
    [Route("api/v{version:apiVersion}/lands")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class FarmLandsController : ControllerBase
    {
        private IMediator _mediator;

        public FarmLandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[AllowAnonymous]
        [HttpGet("measures")]
        public async Task<IActionResult> GetMeasureUnit()
        {


            return Ok(new DefaultResponse<List<string>>
            {
                Data = LengthUnit.All.ToList(),
                Status = 200,
            });
        }

        /*
        TODO:
            - for anonymous user 
        */

        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [Required][FromQuery]Guid siteId, 
            Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            if(id == null) {

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetLandsBySiteCodeQuery
                {
                    SiteId = identity == SystemIdentity.Supervisor? siteId:sId
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<LandResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetLandBySiteCodeQuery { 
                SiteId = siteId,
                Id = id.Value
            });

            return Ok(new DefaultResponse<LandResponse>
            {
                Data = item,
                Status = 200
            });

        }

        [HttpGet("with-properties")]
        public async Task<IActionResult> GetLandWithProperties(Guid? id = null)
        {
            if(id == null)
            {
                var items = await _mediator.Send(new GetLandsWithPropertiesQuery { });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<PagedList<LandWithPropertiesResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetLandWithPropertiesQuery { 
                Id = id.Value
            });

            return Ok(item);
            
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] LandRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }

            var rs = await _mediator.Send(new AddNewLandCommand
            {
                Land = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId!.Value : sId
            });

            return StatusCode(201, new DefaultResponse<LandResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        [HttpPost("add-contract")]
        public async Task<IActionResult> Supply(
            [FromQuery][Required] Guid id,
            [FromBody] SupplyContractRequest request,
            [FromQuery] Guid? siteId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                return StatusCode(404);
            }

            var rs = await _mediator.Send(new MakeLandContractCommand
            {
                Id = id,
                Details = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId
            });

            return StatusCode(200, new DefaultResponse<LandResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpPost("set-position")]
        public async Task<IActionResult> AddPosition(
            [FromQuery][Required] Guid landId,
            [FromBody] List<PositionPoint> request,
            [FromQuery] Guid? siteId = null)
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);

            if (identity == SystemIdentity.Supervisor && siteId == null)
            {
                throw new BadRequestException($"{"siteId"} is required!");
            }

            var rs = await _mediator.Send(new SetPositionCommand
            {
                Positions = request,
                SiteId = identity == SystemIdentity.Supervisor ? siteId.Value : sId,
                LandId = landId

            });

            return StatusCode(201, new DefaultResponse<LandResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put(
            [FromQuery][Required]Guid id, 
            [FromBody] LandRequest request)
        {

            var rs = await _mediator.Send(new UpdateLandCommand
            {
                Id = id,
                Land = request
            });

            return Ok(new DefaultResponse<LandResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteLandCommand { Id = id });

            return NoContent();
        }
    }
}
