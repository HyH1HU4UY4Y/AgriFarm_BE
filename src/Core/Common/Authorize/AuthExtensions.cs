using SharedApplication.Authorize.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Defaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharedApplication.Authorize.Services;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace SharedApplication.Authorize
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<ITokenGenerator>(new TokenGenerator(AppToken.Key, AppToken.Issuer, AppToken.Audience, AppToken.ExpiredIn));
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddJWTAuthorization();

            services.AddTransient<IClaimsTransformation, FarmClaimsTransformation>();
            /*
                        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            */
            return services;
        }
        
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = TokenHelperExtensions.GetValidateParameter();
                o.MapInboundClaims = false;
            });

            

            return services;
        }

        
    }
}
