using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Service.Seed.DTOs
{
    public class SeedInfoRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }

        [MaxLength(8000)]
        public string? Notes { get; set; }
    }
}
