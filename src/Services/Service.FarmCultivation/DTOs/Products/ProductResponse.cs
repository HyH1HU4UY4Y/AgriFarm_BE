using Newtonsoft.Json;
using Service.FarmCultivation.DTOs.Seasons;

namespace Service.FarmCultivation.DTOs.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? TotalQuantity { get; set; } = 0;
        [JsonProperty("currentQuanlity")]
        public double? Quantity { get; set; } = 0;

        public LandVM Land { get; set; }
        public SeasonResponse Season { get; set; }
        public SeedVM Seed { get; set; }

    }
}
