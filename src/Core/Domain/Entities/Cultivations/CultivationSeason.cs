using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PostHarvest;

namespace SharedDomain.Entities.Cultivations
{
    public class CultivationSeason : BaseEntity, ITraceableItem
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }



        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<CultivationProduct> Products { get; set; }
    }
}
