using Newtonsoft.Json;

namespace Service.Supply.DTOs
{
    public class SupplyDetailResponse
    {
        public Guid Id { get; set; }
        public SupplierResponse Supplier { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? ValidFrom { get; set; } = null;
        public DateTime? ValidTo { get; set; } = null;
        public DateTime CreatedDate {  get; set; }
    }
}
