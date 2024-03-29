﻿using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.CORS
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddDomainCors(this IServiceCollection services, string corsName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsName, builder => builder
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders(AdditionHeader.Pagination)
                    .SetIsOriginAllowed(origin => true));
            });
            return services;
        }
    }
}
