using SharedDomain.Defaults;
using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules
{
    public abstract class AdditionOfActivity : BaseEntity
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public AdditionType AdditionType { get; set; }
    }
}
