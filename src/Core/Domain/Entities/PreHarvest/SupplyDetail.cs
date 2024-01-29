using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.PreHarvest
{
    public class SupplyDetail : BaseEntity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Guid SiteId {  get; set; }

        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Unit {  get; set; }
        public bool IsLimitTime { get; set; } = false;
        public DateTime? ExpiredIn { get; set; }
    }
}
