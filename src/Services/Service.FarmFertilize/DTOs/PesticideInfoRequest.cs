using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.DTOs
{
    public class FertilizeInfoRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }

        [MaxLength(8000)]
        public string? Notes { get; set; }
    }
}
