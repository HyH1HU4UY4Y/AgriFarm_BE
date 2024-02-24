namespace Service.Water.DTOs
{
    public class WaterResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Notes { get; set; }
    }
}
