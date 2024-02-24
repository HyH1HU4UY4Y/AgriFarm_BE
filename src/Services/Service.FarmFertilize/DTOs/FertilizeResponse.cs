using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents.Common;

namespace Service.Fertilize.DTOs
{
    public class FertilizeResponse
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }

        [JsonProperty("measureUnit")]
        public string? Unit { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public List<PropertyValue> Properties { get; set; }
    }
}
