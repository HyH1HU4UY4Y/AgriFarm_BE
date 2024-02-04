using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PostHarvest;
using SharedDomain.Entities.Schedules.Cultivations;

namespace SharedDomain.Entities.Schedules
{
    public class CultivationSeason : BaseEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }



        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }

        
        public ICollection<HarvestProduct> Products { get; set; }
    }
}
