using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Service.Seed.DTOs
{
    public class SeedRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [JsonProperty("measureUnit")]
        public string? Unit { get; set; }
        
        [MaxLength(8000)]
        public string? Notes { get; set; }
        public Guid? InfoId { get; set; }
    }
}
