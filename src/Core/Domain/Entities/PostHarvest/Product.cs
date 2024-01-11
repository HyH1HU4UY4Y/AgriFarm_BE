using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;

namespace SharedDomain.Entities.PostHarvest
{
    public class Product : BaseEntity, ITraceableItem
    {

        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<OrderDetail> Orders { get; set; }
        public ICollection<CultivationProduct> Cultivations { get; set; }
    }
}
