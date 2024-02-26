using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.ChecklistGlobalGAP.DTOs;
using Service.ChecklistGlobalGAP.Queries;

namespace Service.ChecklistGlobalGAP.Controllers
{
    // [Authorize]
    [Route("api/v{version:apiVersion}/checklist-global-GAP")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ChecklistGlobalGAPController : ControllerBase
    {
        private IMediator _mediator;
        public ChecklistGlobalGAPController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> GetList([FromQuery] ChecklistGlobalGAPGetListRequest request)
        {
            ChecklistGlobalGAPGetListResponse respose = new ChecklistGlobalGAPGetListResponse();
            var rsAll = await _mediator.Send(new GetListChecklistGlobalGAPQuery
            {
                userId = request.userId
            });
            return Ok();
        }
    }
}
