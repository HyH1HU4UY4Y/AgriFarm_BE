using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Registration.Queries;

namespace Service.FarmRegistry.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private IMediator _mediator;

        public SolutionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _mediator.Send(new GetSolutionsQuery());

            return Ok(rs);
        }
    }
}
