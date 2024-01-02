using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public class PropertyState : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        public string Data { get; set; }
    }
}
