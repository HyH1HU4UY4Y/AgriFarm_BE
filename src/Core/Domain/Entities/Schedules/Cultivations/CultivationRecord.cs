using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Cultivations
{
    public class CultivationRecord: AdditionOfActivity
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }

        public string Instructions { get; set; }
        public string Resources { get; set; }
        public double UseValue { get; set; }
        public string Unit { get; set; }
        public string? Notes { get; set; }
    }
}
