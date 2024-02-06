using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FarmRegistry.DTOs;
using Service.Registration.Queries;
using SharedApplication.Pagination;
using SharedDomain.Common;

namespace Service.FarmRegistry.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private IMediator _mediator;

        public SolutionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
        )
        {
            PaginationRequest page = new(pageNumber, pageSize);

            var rs = await _mediator.Send(new GetSolutionsQuery
            {
                Pagination = page
            });

            return Ok(new DefaultResponse<PagedList<SolutionResponse>>
            {
                Data = rs,
                Status = 200
            });
        }
    }
}
