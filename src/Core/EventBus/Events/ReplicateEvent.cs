using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class ReplicateEvent<T>
    {
        public T Item { get; set; }
    }
}
