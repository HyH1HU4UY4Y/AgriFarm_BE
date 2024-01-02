using SharedDomain.Base;
using SharedDomain.Defaults;
using SharedDomain.FarmComponents;

namespace SharedDomain.Cultivations
{
    public class Schedule : BaseEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public Guid? SeasonId { get; set; }
        public CultivationSeason? Season { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public ScheduleType Type { get; set; } = ScheduleType.Cultivation;
        public string Notes { get; set; }


        public ICollection<Activity> Activities { get; set; }
        public ICollection<ScheduleResource> Resources { get; set; }

    }
}
