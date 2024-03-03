using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Certificates;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedApplication.Authorize;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Common;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;


namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/user-cert")]
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
        public async Task<IActionResult> Get(
            [FromQuery] Guid? userId = null,
            [FromQuery] Guid? id = null,
            [FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null)
        {

            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            userId = (!User.IsInRole(Roles.Member) || User.IsInRole(Roles.SuperAdmin))
                        && userId != null?
                        userId : uId;

            if (id == null)
            {

                PaginationRequest page = new(pageNumber, pageSize);

                var items = await _mediator.Send(new GetCertificatesQuery
                {
                    UserId = userId.Value,
                    Pagination = page
                });

                Response.AddPaginationHeader(items.MetaData);

                return Ok(new DefaultResponse<List<CertificateDetailResponse>>
                {
                    Status = 200,
                    Data = items

                });
            }

            var item = await _mediator.Send(new GetCertificateByIdQuery
            {
                Id = id.Value
            });

            return Ok(new DefaultResponse<CertificateDetailResponse>
            {
                Data = item,
                Status = 200
            });
        }

        
        
        [HttpPost("post")]
        public async Task<IActionResult> Post(
            [FromBody] CertificateRequest request,
            [FromQuery] Guid? userId = null
            )
        {
            var identity = HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            
            userId = (!User.IsInRole(Roles.Member) || User.IsInRole(Roles.SuperAdmin))
                    && userId != null ?
                    userId : uId;
            
            

            var rs = await _mediator.Send(new AddCertificateCommand
            {
                Certificate = request,
                UserId = userId.Value,
            });

            return StatusCode(201, new DefaultResponse<CertificateDetailResponse>
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery][Required]Guid id)
        {
            await _mediator.Send(new RemoveCertificateCommand { Id = id});
            return NoContent();
        }
    }
}
