using System.ComponentModel.DataAnnotations;

namespace Service.FarmCultivation.DTOs
{
    public class SeasonRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }
    }
}
