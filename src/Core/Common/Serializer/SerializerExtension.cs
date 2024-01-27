using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Serializer
{
    public static class SerializerExtension
    {
        public static IMvcBuilder AddDefaultJson(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.ContractResolver = null;
                
            });
/*
            builder.AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
            });*/
            

            return builder;
        }

    }
}
