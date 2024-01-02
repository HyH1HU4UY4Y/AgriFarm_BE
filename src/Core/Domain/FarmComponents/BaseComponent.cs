using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public abstract class BaseComponent : BaseEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Notes { get; set; }


        public ICollection<ComponentProperty> Properties { get; set; }
        public ICollection<PropertyState> States { get; set; }
        public ICollection<ComponentDocument> Documents { get; set; }
    }
}
