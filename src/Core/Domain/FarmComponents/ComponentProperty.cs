using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public class ComponentProperty : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Require { get; set; }

        public string Unit { get; set; }

    }
}
