using SharedDomain.Base;

namespace SharedDomain.PostHarvest
{
    public class OrderDetail : BaseEntity, ITraceableItem
    {

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
