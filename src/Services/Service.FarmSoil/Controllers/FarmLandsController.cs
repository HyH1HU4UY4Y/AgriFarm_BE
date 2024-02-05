
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Soil.Command;
using Service.Soil.DTOs;
using Service.Soil.Queries;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Soil.Controllers
{
    [Route("api/v{version:apiVersion}/lands")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FarmLandsController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public FarmLandsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("get")]
        public async Task<IActionResult> Get([Required][FromQuery]string siteCode, Guid? id = null)
        {
            if(id == null) {

                var items = await _mediator.Send(new GetLandsBySiteCodeQuery
                {
                    SiteCode = siteCode
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<PagedList<LandResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetLandBySiteCodeQuery { 
                SiteCode = siteCode,
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
        public async Task<IActionResult> Post([FromBody] AddNewLandCommand request)
        {
            var rs = await _mediator.Send(request);

            return StatusCode(201, rs);
        }


        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery]Guid id, [FromBody] LandRequest request)
        {
            var cmd = _mapper.Map<UpdateLandCommand>(request);
            cmd.Id = id;

            await _mediator.Send(cmd);

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteLandCommand { Id = id });

            return NoContent();
        }
    }
}
