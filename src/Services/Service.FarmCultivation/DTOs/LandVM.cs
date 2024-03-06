using System.ComponentModel.DataAnnotations;

namespace Service.FarmCultivation.DTOs
{
    public class LandVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
