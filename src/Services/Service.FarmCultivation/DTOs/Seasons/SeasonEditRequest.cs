using System.ComponentModel.DataAnnotations;

namespace Service.FarmCultivation.DTOs.Seasons
{
    public class SeasonEditRequest
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
    }
}
