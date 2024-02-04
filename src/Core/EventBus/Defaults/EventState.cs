using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Defaults
{
    public enum EventState
    {
        None,
        Add,
        Modify,
        RawDelete,
        SoftDelete
    }
}
