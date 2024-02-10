using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.DTOs;
using Service.Identity.Queries;
using SharedApplication.Authorize;
using SharedApplication.Pagination;
using SharedDomain.Common;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Identity.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[AllowAnonymous]
        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] Guid id
            //[FromHeader] int? pageNumber = null, [FromHeader] int? pageSize = null
        )
        {
            HttpContext.User.TryCheckIdentity(out var uId, out var sId);
            

            /*if (id == null)
            {

                PaginationRequest page = new(pageNumber, pageSize);

                var users = await _mediator.Send(new GetStaffsQuery
                {
                    SiteId = sId,
                    Pagination = page
                });

                Response.AddPaginationHeader(users.MetaData);

                return Ok(new DefaultResponse<List<UserResponse>>
                {
                    Status = 200,
                    Data = users

                });
            }*/

            var user = await _mediator.Send(new GetMemberByIdQuery
            {
                SiteId = sId,
                UserId = id
            });

            return Ok(new DefaultResponse<UserDetailResponse>
            {
                Data = user,
                Status = 200
            });

        }


        // POST api/<AccountsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
