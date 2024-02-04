using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Training.Commands.TrainingDetails;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingDetails;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Training.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TrainingDetailsController : ControllerBase
    {
        private IMediator _mediator;

        public TrainingDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null)
        {
            //TODO: add check http context and site query string for super admin

            if (id == null)
            {
                var items = await _mediator.Send(new GetTrainingDetailsQuery());

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<TrainingDetailResponse>>
                {
                    Data = items,
                    Status = 200
                });
            }

            var item = await _mediator.Send(new GetTrainingDetailByIdQuery { Id = id.Value });


            return Ok(new DefaultResponse<TrainingDetailResponse>
            {
                Data = item,
                Status = 200
            });

        }



        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] TrainingDetailRequest request)
        {
            var rs = await _mediator.Send(new AddTrainingDetailCommand { TrainingDetail = request });

            return StatusCode(201);
        }

        [HttpPut("put")]
        public async Task<IActionResult> Put([Required][FromQuery] Guid id, [FromBody] TrainingDetailRequest request)
        {

            var rs = await _mediator.Send(new UpdateTrainingDetailCommand
            {
                Id = id,
                TrainingDetail = request
            });

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
        {
            var rs = await _mediator.Send(new RemoveTrainingDetailCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }

}
