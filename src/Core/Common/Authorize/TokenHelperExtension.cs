using Microsoft.IdentityModel.Tokens;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Authorize
{
    public class TokenHelperExtension
    {
        public static TokenValidationParameters GetValidateParameter()
        {
            return new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = AppToken.Audience,
                ValidIssuer = AppToken.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppToken.Key)),
                ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(AppToken.ExpiredIn))
            };
        }
    }
}
