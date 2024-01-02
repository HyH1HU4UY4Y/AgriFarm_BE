using Microsoft.AspNetCore.Identity;
using SharedDomain.Cultivations;
using SharedDomain.FarmComponents;

namespace SharedDomain.Users
{
    public class Member : IdentityUser<Guid>
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public ICollection<ActivityWorker> Activities { get; set; }
    }
}
