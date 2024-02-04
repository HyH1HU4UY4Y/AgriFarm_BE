using EventBus.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public class IntegrationEventMessage<T>
    {
        public IntegrationEventMessage(T @event, EventState state) { 
            this.Data = @event;
            this.State = state;
        }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public T Data { get; set; }
        public EventState State { get; set; } = EventState.None;
    }
}
