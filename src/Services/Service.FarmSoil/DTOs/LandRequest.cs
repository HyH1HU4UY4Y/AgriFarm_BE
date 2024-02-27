using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Soil.DTOs
{
    public class LandRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = "not set";
        [Range(1,double.PositiveInfinity)]
        public double Acreage { get; set; }
        [Required]
        [JsonProperty("defaultUnit")]
        public string Unit {  get; set; }
        
    }
}
