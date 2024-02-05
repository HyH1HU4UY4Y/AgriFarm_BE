using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddSharedApplication<T>(this IServiceCollection services)
        {
            var applicationAssembly = typeof(T).Assembly;
            services.AddMediatR(applicationAssembly);
            services.AddAutoMapper(applicationAssembly);
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
