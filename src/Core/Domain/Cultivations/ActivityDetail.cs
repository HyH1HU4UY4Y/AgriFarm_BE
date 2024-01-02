using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.Cultivations
{
    public class ActivityDetail : BaseEntity, ITraceableItem
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
