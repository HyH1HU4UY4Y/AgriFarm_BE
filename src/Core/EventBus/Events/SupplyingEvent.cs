using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class SupplyingEvent
    {
        public SupplierDetail Supplier {  get; set; }
        public SupplyItem Item { get; set; }
        public string? Content { get; set; }
        public Guid SiteId { get; set; }
        public double Quanlity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; }
        public DateTime? ValidFrom { get; set; } = null;
        public DateTime? ValidTo { get; set; } = null;
        
    }

    public class SupplierDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    
    public class SupplyItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}
