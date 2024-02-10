using Infrastructure.Identity.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedApplication.Authorize;
using SharedApplication.MultiTenant;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using System.Net;

namespace Infrastructure.Identity
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDefaultSQLDB<IdentityContext>(configuration);

            services.AddIdentity<Member, IdentityRole<Guid>>(o =>
            {

            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            services.AddSQLRepo<IdentityContext, Site>()
                    .AddSQLRepo<IdentityContext, Certificate>()
                    .AddMultiTenant(configuration);
                    

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
                

                
            });

            services.Configure<CookieAuthenticationOptions>(o =>
            {
                o.Events.OnRedirectToAccessDenied = c =>
                {
                    c.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.FromResult<object>(null);
                };
                o.Events.OnRedirectToLogin = c =>
                {
                    c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.FromResult<object>(null);
                };
                
            });


            //services.AddAuthModule(configuration);

            return services;
        }


        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            

            return app;
        }
    }
}
