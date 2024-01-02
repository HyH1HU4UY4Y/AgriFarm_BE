using SharedDomain.Base;
using SharedDomain.Defaults;

namespace SharedDomain.Cultivations
{
    public class ScheduleResource : BaseEntity, ITraceableItem
    {
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public ScheduleResourceType Type { get; set; } = ScheduleResourceType.None;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }


    }
}
