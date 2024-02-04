using SharedDomain.Defaults;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Users
{
    public class Certificate : BaseEntity, ITraceableItem
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public Guid? HandlerId { get; set; }
        public Member? Handler { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public DecisonOption Decison { get; set; } = DecisonOption.Waiting;
        public string Resource { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
