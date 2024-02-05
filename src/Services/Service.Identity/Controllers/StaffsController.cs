using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Commands.Users;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedDomain.Common;
using SharedDomain.Defaults;
using SharedApplication.Pagination;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class StaffsController : ControllerBase
    {
        private IMediator _mediator;

        public StaffsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetStaff(
            [FromQuery][Required]Guid siteId, 
            [FromQuery]Guid? userId = null,
            [FromHeader]int? pageNumber = null, [FromHeader]int? pageSize = null)
        {
            if (userId == null)
            {
                var cmd = new GetStaffsQuery { SiteId = siteId };
                if(pageNumber != null && pageSize != null)
                {
                    cmd.Pagination = new() {
                        PageNumber = (int)pageNumber,
                        PageSize = (int)pageSize
                    };
                }

                var users = await _mediator.Send(cmd);

                Response.AddPaginationHeader(users.MetaData);

                return Ok(new DefaultResponse<List<UserResponse>>
                {
                    Status = 200,
                    Data = users

                });
            }

            var user = await _mediator.Send(new GetMemberByIdQuery 
            { 
                SiteId = siteId, 
                UserId = (Guid)userId 
            });

            return Ok(new DefaultResponse<UserDetailResponse>
            {
                Data = user,
                Status = 200
            });
            
        }


        [HttpPost("add-new-staff")]
        public async Task<IActionResult> AddNewStaff([FromQuery]Guid siteId, [FromBody] AddStaffRequest request)
        {
            var rs = await _mediator.Send(new CreateMemberCommand
            {
                SiteId = siteId,
                Staff = request,
                AccountType = AccountType.Member
            });

            return StatusCode(201,new DefaultResponse<UserResponse>
            {
                Status =201,
                Data = rs

            });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditStaff(
            [Required][FromQuery]Guid id
            , [FromBody] SaveMemberDetailRequest request)
        {
            
            var rs = await _mediator.Send(new UpdateMemberCommand
            {
                Id = id,
                User = request
            });

            return Ok(new DefaultResponse<UserDetailResponse>
            {
                 Data= rs,
                 Status = 200
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteMemberCommand {
                Id = id 
            });

            return NoContent();
        }
    }
}
