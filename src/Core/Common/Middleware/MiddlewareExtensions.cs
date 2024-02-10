using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddGlobalErrorMiddleware(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            return services;
        }

        
        public static IApplicationBuilder UseGlobalErrorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }

        public static IServiceCollection AddIdentityClaimsMiddleware(this IServiceCollection services)
        {
            services.AddTransient<IdentityClaimsMiddleware>();
            return services;
        }


        public static IApplicationBuilder UseIdentityClaimsMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<IdentityClaimsMiddleware>();
            return app;
        }
    }
}
