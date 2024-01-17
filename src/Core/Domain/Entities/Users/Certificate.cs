using SharedDomain.Defaults;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Users
{
    public class Certificate : BaseEntity
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public Guid? InspectorId { get; set; }
        public Member? Inspector { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public DecisonOption Decison { get; set; } = DecisonOption.Waiting;
        public string Resource { get; set; }

    }
}
