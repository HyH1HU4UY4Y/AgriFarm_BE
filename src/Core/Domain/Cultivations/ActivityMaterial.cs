using SharedDomain.Base;
using SharedDomain.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Cultivations
{
    public class ActivityMaterial : BaseEntity
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }

        public double UseValue { get; set; }
        public string Unit { get; set; }
    }
}
