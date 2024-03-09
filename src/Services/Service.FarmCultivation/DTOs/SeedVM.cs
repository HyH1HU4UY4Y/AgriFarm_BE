using System.ComponentModel.DataAnnotations;

namespace Service.FarmCultivation.DTOs
{
    public class SeedVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
