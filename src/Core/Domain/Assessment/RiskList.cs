using SharedDomain.Base;
using SharedDomain.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Assessment
{
    public class RiskList : BaseEntity, ITraceableItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }


        public Guid SiteId { get; set; }
        public Site Site { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<RiskDetail> Details { get; set; }
    }
}
