using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.DTOs
{
    public class RefFertilizeRequest
    {
        [Required]
        public string Name { get; set; }
        [StringLength(8000)]
        public string? Description { get; set; } = "not set";
        [StringLength(500)]
        public string? Manufactory { get; set; } = "not set";
        public DateTime? ManufactureDate { get; set; }
        [MaxLength(100)]
        public List<PropertyValue> Properties { get; set; }

        [StringLength(8000)]
        public string? Notes { get; set; } = "not set";
        [StringLength(1000)]
        [JsonProperty("image")]
        public string? Resources { get; set; }
    }
}
