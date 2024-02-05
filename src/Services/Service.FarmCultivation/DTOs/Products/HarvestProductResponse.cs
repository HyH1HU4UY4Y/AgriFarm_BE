namespace Service.FarmCultivation.DTOs.Products
{
    public class HarvestProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public LandResponse Land { get; set; }
        public SeasonResponse Season { get; set; }
        public SeedResponse Seed { get; set; }

    }
}
