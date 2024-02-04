using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public static class EventBusExtensions
    {
        public static async Task<IBus> SendToEndpoint<T>(this IBus bus, T data, string endpoint) where T : class
        {
            var endPoint = await bus.GetSendEndpoint(new Uri($"exchange:{endpoint}"));
            await endPoint.Send<T>(data);
            return bus;
        }
    }
}
