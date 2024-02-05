using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Authorize

{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetEmail(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.Email);



        public static string? GetFirstName(this ClaimsPrincipal principal)
            => principal?.FindFirst(FarmClaimType.FirstName)?.Value;

        public static string? GetSurname(this ClaimsPrincipal principal)
            => principal?.FindFirst(FarmClaimType.LastName)?.Value;

        public static string? GetPhoneNumber(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.MobilePhone);

        public static string? GetUserId(this ClaimsPrincipal principal)
           => principal.FindFirstValue(ClaimTypes.NameIdentifier);

        public static string? GetSiteId(this ClaimsPrincipal principal)
           => principal.FindFirstValue(FarmClaimType.SiteId);
        
        public static string? GetScope(this ClaimsPrincipal principal)
           => principal.FindFirstValue(FarmClaimType.Scope);



        private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
            principal is null
                ? throw new ArgumentNullException(nameof(principal))
                : principal.FindFirst(claimType)?.Value;
    }
}
