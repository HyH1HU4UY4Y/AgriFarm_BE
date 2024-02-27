using System.ComponentModel.DataAnnotations;

namespace Service.Equipment.DTOs
{
    public class SupplyContractRequest
    {
        public string? Content { get; set; } = "none";
        public string? Resource { get; set; }
        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal Price { get; set; }
        [Required]
        public SupplierRequest Supplier { get; set; }
        public DateTime? ValidFrom { get; set; } = null;
        public DateTime? ValidTo { get; set; } = null;

    }

    public class SupplierRequest
    {
        public Guid? Id { get; set; } = Guid.Empty;
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
