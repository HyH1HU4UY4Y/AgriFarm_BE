using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.PostHarvest
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }

        public ICollection<Order> Orders;
    }
}
