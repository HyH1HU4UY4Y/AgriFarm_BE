namespace Service.Supply.DTOs
{
    public class NewContractRequest
    {
        public Guid SupplierId { get; set; }
        public Guid ComponentId { get; set; }
        public string? Content { get; set; }
        public string? Resource { get; set; }
        public decimal UnitPrice { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public bool IsLimitTime { get; set; } = false;
        public DateTime? ExpiredIn { get; set; }
    }
}
