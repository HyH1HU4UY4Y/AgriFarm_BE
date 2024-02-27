using System.ComponentModel.DataAnnotations;

namespace Service.Supply.DTOs
{
    public class SupplierRequest
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(8000)]
        public string? Description { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(500)]
        public string? Address { get; set; }
        [StringLength(5000)]
        public string? Notes { get; set; }
    }
}
