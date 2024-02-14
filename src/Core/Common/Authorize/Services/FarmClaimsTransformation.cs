using Microsoft.AspNetCore.Authentication;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Authorize.Services
{
    internal class FarmClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            

            foreach (var claim in principal.Claims)
            {
                switch (claim.Type)
                {
                    case FarmClaimType.SiteId:
                        claimsIdentity.AddClaim(new Claim(FarmClaimType.SiteId, claim.Value));
                        break;
                    case FarmClaimType.FirstName:
                        claimsIdentity.AddClaim(new Claim(FarmClaimType.FirstName, claim.Value));
                        break;
                    case FarmClaimType.LastName:
                        claimsIdentity.AddClaim(new Claim(FarmClaimType.LastName, claim.Value));
                        break;
                    default:
                        break;
                }
            }


            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
