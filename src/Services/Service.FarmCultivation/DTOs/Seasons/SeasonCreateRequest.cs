using Newtonsoft.Json;
using Service.FarmCultivation.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace Service.FarmCultivation.DTOs.Seasons
{
    public class SeasonCreateRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime StartIn { get; set; }
        [Required]
        public DateTime EndIn { get; set; }
        [MaxLength(5)]
        [JsonProperty("cultivationDetail")]
        public List<ProductRequest> Products { get; set; } = new();
    }
}
