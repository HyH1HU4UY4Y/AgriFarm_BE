using Newtonsoft.Json;

namespace Service.Supply.DTOs
{
    public class SupplyDetailResponse
    {
        public Guid Id { get; set; }
        public SupplierResponse Supplier { get; set; }
        [JsonProperty("itemId")]
        public Guid ComponentId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string ExpiredIn { get; set; }
    }
}
