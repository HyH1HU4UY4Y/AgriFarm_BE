using SharedDomain.Entities.Base;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Cultivations
{
    public class ActivityWorker : BaseEntity, ITraceableItem
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }

        public Guid WorkerId { get; set; }
        public Member Worker { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
