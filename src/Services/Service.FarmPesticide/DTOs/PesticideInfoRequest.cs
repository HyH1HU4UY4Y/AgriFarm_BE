using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.DTOs
{
    public class PesticideInfoRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }

        [MaxLength(8000)]
        public string? Notes { get; set; }
    }
}
