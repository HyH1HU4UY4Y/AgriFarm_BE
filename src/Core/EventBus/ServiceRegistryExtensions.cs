using EventBus.Defaults;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddDefaultEventBusExtension<T>(this IServiceCollection services
            , IConfiguration configuration
            , Action<IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator>, IBusRegistrationContext> receiverConfig)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(T).Assembly);
                x.UsingRabbitMq((context, configurator) =>
                {
                    var setting = configuration.GetRequiredSection(nameof(EventBusSetting)).Get<EventBusSetting>();

                    configurator.ClearSerialization();
                    configurator.AddNewtonsoftRawJsonSerializer();

                    configurator.Host(setting!.Host, hostConfigurator =>
                    {
                        hostConfigurator.Username(setting.UserName);
                        hostConfigurator.Password(setting.Password);
                    });
                    /*configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(1, TimeSpan.FromMinutes(5));
                    });*/

                    receiverConfig(configurator, context);
                });
                

                
            });
            

            return services;
        }


        public static IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator> AddReceiveEndpoint<T>(this IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator> receiveConfigurator,
            string endpoint, IBusRegistrationContext context) where T : class, IConsumer
        {
            receiveConfigurator.ReceiveEndpoint(endpoint, x =>
            {
                x.ConfigureConsumer<T>(context);

            });
            return receiveConfigurator;
        }

    }
}
