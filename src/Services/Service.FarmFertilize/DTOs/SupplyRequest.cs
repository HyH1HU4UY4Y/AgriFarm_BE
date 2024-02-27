using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.DTOs
{
    public class SupplyRequest
    {
        [Required]
        [Range(1, 10000)]
        public int Quanlity { get; set; }
        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal UnitPrice { get; set; }
        [Required]
        [JsonProperty("measureUnit")]
        public string Unit { get; set; }
        [MaxLength(500)]
        public string? Content { get; set; }
        [Required]
        public SupplierRequest Supplier { get; set; }
    }

    public class SupplierRequest
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
