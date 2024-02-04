using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents;
using System.Text.Json.Serialization;

namespace Service.Soil.DTOs
{
    public class LandRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Acreage { get; set; }
        [JsonProperty("measureUnit")]
        //[JsonPropertyName("measureUnit")]
        public string Unit {  get; set; }
        public List<PositionPoint> Positions { get; set; }
    }
}
