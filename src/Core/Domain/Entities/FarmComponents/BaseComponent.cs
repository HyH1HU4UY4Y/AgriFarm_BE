using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents.Others;

namespace SharedDomain.Entities.FarmComponents
{
    public class BaseComponent : BaseEntity, IMultiSite
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; } = "not set";
        public bool IsConsumable { get; set; } = false;
        [Column("MeasureUnit")]
        public string? Unit { get; set; }
        [MaxLength(8000)]
        public string? Notes { get; set; } = "not set";
        [MaxLength(int.MaxValue)]
        public string? Resource { get; set; }

        public ICollection<ComponentProperty> Properties { get; set; }
        public ICollection<ComponentState> States { get; set; }
        public ICollection<ComponentDocument> Documents { get; set; }
    }
}
