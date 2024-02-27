using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.DTOs
{
    public class PesticideCreateRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
        [Required]
        [JsonProperty("defaultUnit")]
        public string Unit { get; set; }

        [MaxLength(8000)]
        public string? Notes { get; set; }
        public Guid? ReferenceId { get; set; }
        [MaxLength(10), MinLength(1)]
        public List<PropertyValue> Properties { get; set; }
    }
}
