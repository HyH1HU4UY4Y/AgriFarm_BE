using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.PreHarvest
{
    public class SupplyDetail : BaseEntity, IMultiSite
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Guid SiteId {  get; set; }

        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }

        [MaxLength(8000)]
        public string? Content { get; set; }

        private string _resource;
        [MaxLength(8000)]
        public string? Resource { get => _resource; set => _resource = value??""; }

        [NotMapped]
        public List<string> Resources {
            get => string.IsNullOrWhiteSpace(_resource)? 
                JsonConvert.DeserializeObject<List<string>>(_resource)!
                : new(); 
            set => _resource = JsonConvert.SerializeObject(value); 
        }
        public decimal UnitPrice { get; set; }
        public double Quantity { get; set; }
        public string Unit {  get; set; }
        public DateTime? ValidFrom { get; set; } = null;
        public DateTime? ValidTo { get; set; } = null;

    }
}
