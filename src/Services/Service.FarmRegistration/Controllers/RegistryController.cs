using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.FarmRegistry.Commands;
using Service.FarmRegistry.DTOs;
using Service.Registration.DTOs;
using Service.Registration.Queries;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmRegistry.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = Roles.SuperAdmin)]
    public class RegistryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public RegistryController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
        )
        {
            PaginationRequest page = new(pageNumber, pageSize);
            var rs = await _mediator.Send(new GetRegisterFormsQuery { 
                Pagination = page
            });

            Response.AddPaginationHeader(rs.MetaData);
            return Ok(new DefaultResponse<PagedList<RegisterFormResponse>>{
                Data = rs,
                Status = 200
            });
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistFarmCommand request)
        {
            var rs = await _mediator.Send(request);

            return Accepted(new DefaultResponse<RegisterFormResponse>
            {
                Data = rs,
                Status = 202
            });
        }

        

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery]Guid id, [FromBody] ResolveFormRequest request)
        {
            
            var rs = await _mediator.Send(new ResolveFormCommand
            {
                Id = id,
                Decison = request.Decison,
                Notes = request.Notes
            });

            /*return Ok(new DefaultResponse<Guid>
            {
                Data = rs,
                Status = 200
            });*/
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return NoContent();
        }
    }
}
