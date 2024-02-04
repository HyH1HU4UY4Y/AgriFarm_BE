using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.PostHarvest
{
    public class ProductTraceability : BaseEntity, ITraceableItem
    {
        public Guid ProductId { get; set; }
        public CultivationProduct Product { get; set; }

        public string Provider { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public bool IsActive { get; set; }
        public DateTime ExpiredIn { get; set; } = DateTime.Now.AddMonths(6);
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

    }
}
