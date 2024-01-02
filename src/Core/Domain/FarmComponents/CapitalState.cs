using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public class CapitalState : BaseEntity, ITraceableItem
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
