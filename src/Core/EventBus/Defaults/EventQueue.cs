using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Defaults
{
    public sealed class EventQueue
    {
        public const string RegistFarmQueue= nameof(RegistFarmQueue);
        public const string InitFarmOwner= nameof(InitFarmOwner);
    }
}
