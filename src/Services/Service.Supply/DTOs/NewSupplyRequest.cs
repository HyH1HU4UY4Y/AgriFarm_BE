namespace Service.Supply.DTOs
{
    public class NewSupplyRequest
    {
        public Guid SupplierId { get; set; }
        public Guid ComponentId { get; set; }
        public string? Content { get; set; }
        public List<string> Resources { get; set; }
        public decimal UnitPrice { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
