using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.FarmRegistry.Commands;
using Service.FarmRegistry.DTOs;
using Service.Registration.Queries;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.FarmRegistry.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public RegistryController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/<RegistryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _mediator.Send(new GetRegisterFormsQuery()); 

            return Ok(rs);
        }

        // GET api/<RegistryController>/5
        [HttpGet("by-id")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        // POST api/<RegistryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistFarmCommand request)
        {
            var rs = await _mediator.Send(request);

            return Accepted(rs);
        }

        // PUT api/<RegistryController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromQuery]Guid id, [FromBody] ResolveFormCommand request)
        {
            request.Id = id;
            var rs = await _mediator.Send(request);

            return Ok(rs);
        }

        // DELETE api/<RegistryController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return NoContent();
        }
    }
}
