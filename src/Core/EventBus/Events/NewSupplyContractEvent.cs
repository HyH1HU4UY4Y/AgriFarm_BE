using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class NewSupplyContractEvent
    {
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }

        public Guid SiteId { get; set; }

        public string? Content { get; set; }
        public string? Resource { get; set; }

        public Guid ComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool IsConsumable { get; set; }

        public decimal UnitPrice { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public bool IsLimitTime { get; set; } = false;
        public DateTime? ExpiredIn { get; set; }
    }
}
