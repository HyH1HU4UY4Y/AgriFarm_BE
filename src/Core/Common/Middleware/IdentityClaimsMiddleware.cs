using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedApplication.Authorize.Services;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Middleware
{
    internal class IdentityClaimsMiddleware : IMiddleware
    {
        private readonly ILogger<IdentityClaimsMiddleware> _logger;
        public IdentityClaimsMiddleware(ILogger<IdentityClaimsMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.Request.Headers.TryGetValue("Authorization", out var authHeader) 
                && !string.IsNullOrWhiteSpace(authHeader))
            {

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(authHeader.ToString().Substring("Bearer ".Length));
                var siteClaim = token.Claims.FirstOrDefault(c=>c.Type == FarmClaimType.SiteId);
                if (siteClaim != null)
                {
                    //var value = siteClaim.Value == "root"? Guid.Empty.ToString():siteClaim.Value;
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(siteClaim);
                    context.User.AddIdentity(identity);
                }
            }
            await next(context);
        }
    }
}
