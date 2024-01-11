using System.ComponentModel.DataAnnotations;

namespace Service.FarmRegistry.DTOs
{
    public class SolutionResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long? DurationHour { get; set; }

    }
}
