using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents.Common;

namespace Service.Pesticide.DTOs
{
    public class RefPesticideResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Manufactory { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public List<PropertyValue> Properties { get; set; }
        public string? Notes { get; set; }
        [JsonProperty("image")]
        public string? Resources { get; set; }
    }
}
