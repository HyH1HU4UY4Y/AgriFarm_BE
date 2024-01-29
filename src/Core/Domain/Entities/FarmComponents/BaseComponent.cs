using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents.Others;

namespace SharedDomain.Entities.FarmComponents
{
    public class BaseComponent : BaseEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; } = "not set";
        public bool IsConsumable { get; set; } = false;
        [Column("Measure Unit")]
        public string? Unit { get; set; }
        public string? Notes { get; set; }


        public ICollection<ComponentProperty> Properties { get; set; }
        public ICollection<ComponentState> States { get; set; }
        public ICollection<ComponentDocument> Documents { get; set; }
    }
}
