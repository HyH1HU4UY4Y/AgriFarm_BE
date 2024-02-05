using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Others
{
    public class ComponentDocument : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

    }
}
