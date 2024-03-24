using SharedDomain.Defaults;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Schedules
{
    public abstract class AdditionOfActivity : BaseEntity
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public string AdditionType { get; set; }

    }
}
