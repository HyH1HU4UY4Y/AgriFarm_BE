/*using SharedApplication.Authorize.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Defaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharedApplication.Authorize.Services;
using SharedApplication.Authorize;

namespace Infrastructure.CommonShared.Authorize
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddIdentityModule<T>(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationAssembly = typeof(T).Assembly;

            services.AddSingleton<ITokenGenerator>(new TokenGenerator(AppToken.Key, AppToken.Issuer, AppToken.Audience, AppToken.ExpiredIn));
            services.AddScoped<IIdentityService, IdentityService>();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = TokenHelperExtension.GetValidateParameter();
            });

            *//*
                        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            *//*
            return services;
        }
    }
}
*/