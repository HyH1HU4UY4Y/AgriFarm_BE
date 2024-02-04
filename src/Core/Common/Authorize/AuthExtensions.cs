using SharedApplication.Authorize.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Defaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharedApplication.Authorize.Services;

namespace SharedApplication.Authorize
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<ITokenGenerator>(new TokenGenerator(AppToken.Key, AppToken.Issuer, AppToken.Audience, AppToken.ExpiredIn));
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddJWTAuthorization();
            

            /*
                        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            */
            return services;
        }
        
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
        {
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = TokenHelperExtensions.GetValidateParameter();
            });

            return services;
        }
    }
}
