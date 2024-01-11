using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SharedPermissionAuth.Authorize.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
       /* private readonly IIdentityService _userService;

        public PermissionAuthorizationHandler(IIdentityService userService) =>
            _userService = userService;
*/
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            /*Console.WriteLine("********* Check!");
            if (context.User?.GetUserId() is { } userId &&
                await _userService.HasPermissionAsync(userId, requirement.Permission))
            {
                Console.WriteLine("------------ Success!");
                context.Succeed(requirement);
            }*/
        }
    }
}
