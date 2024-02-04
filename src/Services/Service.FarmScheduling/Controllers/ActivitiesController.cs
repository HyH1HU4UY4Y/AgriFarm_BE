using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmScheduling.Commands.Activities;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.Queries.Activities;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmScheduling.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ActivitiesController : ControllerBase
    {
        private IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            if(id == null)
            {
                var items = await _mediator.Send(new GetActivitiesQuery
                {
                   
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<ActivityResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            return Ok();
        }

        

        
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] ActivityRequest request)
        {
            var rs = await _mediator.Send(new CreateActivityCommand
            {
                Activity = request,

            });

            return StatusCode(201);
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery]Guid id, [FromBody] ActivityRequest request)
        {

            return NoContent();
        }

        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {

            return NoContent();
        }
    }
}
