using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Authorize.Contracts;
using SharedApplication.Authorize.Services;
using SharedApplication.MultiTenant.Implement;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.MultiTenant
{
    public static class MultiTenantExtensions
    {
        public static IServiceCollection AddMultiTenant(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IMultiTenantResolver, MultiTenantResolver>();
            
            return services;
        }
    }
}
