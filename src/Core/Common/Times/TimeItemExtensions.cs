using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Times.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Times
{
    public static class TimeItemExtensions
    {
        public static IServiceCollection AddTimeModule(this IServiceCollection services)
        {
            services.AddScoped<TimePickerHelper>();

            return services;
        }
    }
}
