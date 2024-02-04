using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Certificates;
using Service.Identity.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/my-cert")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserCertificatesController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public UserCertificatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            

            return Ok();
        }

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CertificateRequest request)
        {
            var cmd = _mapper.Map<AddCertificateCommand>(request);
            var rs = await _mediator.Send(cmd);

            return StatusCode(201);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CertificateRequest request)
        {

            var cmd = _mapper.Map<UpdateCertificateCommand>(request);
            cmd.Id = id;
            var rs = await _mediator.Send(cmd);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveCertificateCommand { Id = id});
            return NoContent();
        }
    }
}
