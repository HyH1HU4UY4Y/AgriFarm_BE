using SharedDomain.Base;
using SharedDomain.FarmComponents;
using SharedDomain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.PostHarvest
{
    public class Order : BaseEntity, ITraceableItem
    {

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public Guid AccountantId { get; set; }
        public Member Accountant { get; set; }

        public decimal Total { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
