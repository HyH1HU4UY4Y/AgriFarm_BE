using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Certificates;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedApplication.Authorize;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/my-cert")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class UserCertificatesController : ControllerBase
    {
        private IMediator _mediator;

        public UserCertificatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]Guid id)
        {
            var rs = await _mediator.Send(new GetCertificateByIdQuery
            {
                Id = id
            });

            return Ok(new DefaultResponse<CertificateDetailResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        
        
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] CertificateRequest request)
        {
            Guid uId = Guid.Empty;
            if (!Guid.TryParse(HttpContext.User.GetUserId(), out uId))
            {
                return Unauthorized();
            }

            var rs = await _mediator.Send(new AddCertificateCommand
            {
                Certificate = request,
                UserId = uId
            });

            return StatusCode(201, new DefaultResponse<CertificateResponse>
            {
                Data = rs,
                Status = 201
            });
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> Put([FromQuery][Required]Guid id, 
            [FromBody] CertificateRequest request)
        {

            var rs = await _mediator.Send(new UpdateCertificateCommand
            {
                Certificate = request,
                Id = id
            });

            return Ok(new DefaultResponse<CertificateDetailResponse>
            {
                Data = rs,
                Status = 200
            });
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveCertificateCommand { Id = id});
            return NoContent();
        }
    }
}
