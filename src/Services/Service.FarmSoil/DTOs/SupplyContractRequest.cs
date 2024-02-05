namespace Service.Soil.DTOs
{
    public class SupplyContractRequest
    {
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string? Content { get; set; }
        public string? Resource { get; set; }
        public decimal Price { get; set; }
        public bool IsLimitTime { get; set; } = false;
        public DateTime? ExpiredIn { get; set; }
    }
}
