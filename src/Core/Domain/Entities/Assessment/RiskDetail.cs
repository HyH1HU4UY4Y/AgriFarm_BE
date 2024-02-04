using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Assessment
{
    public class RiskDetail : BaseEntity, ITraceableItem
    {
        public Guid ListId { get; set; }
        public RiskList List { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
