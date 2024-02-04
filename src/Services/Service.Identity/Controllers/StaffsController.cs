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
        private IMapper _mapper ;

        public StaffsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<StaffsController>
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

            return Ok(new DefaultResponse<UserResponse>
            {
                Data = user,
                Status = 200
            });
            
        }

        
        [HttpGet("value")]
        public string GetValue()
        {
            return "value";
        }

        // POST api/<StaffsController>
        [HttpPost("add-new-staff")]
        public async Task<IActionResult> AddNewStaff([FromQuery]Guid siteId, [FromBody] AddStaffRequest request)
        {
            var rs = await _mediator.Send(new CreateMemberCommand
            {
                SiteId = siteId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName,
                AccountType = AccountType.Member
            });

            return StatusCode(201,new DefaultResponse<Guid>
            {
                Status =201,
                Data = rs

            });
        }

        // PUT api/<StaffsController>/5
        [HttpPut("edit")]
        public async Task<IActionResult> EditStaff(
            [Required][FromQuery]Guid id
            , [FromBody] SaveMemberDetailRequest request)
        {
            var cmd = _mapper.Map<UpdateMemberCommand>(request);
            cmd.Id = id;

            await _mediator.Send(cmd);

            return NoContent();
        }

        // DELETE api/<StaffsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required][FromQuery]Guid id)
        {
            await _mediator.Send(new DeleteMemberCommand { Id = id });

            return NoContent();
        }
    }
}
