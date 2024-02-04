using Microsoft.AspNetCore.Identity;
using SharedDomain.Defaults;
using SharedDomain.Entities.Cultivations;
using SharedDomain.Entities.FarmComponents;

namespace SharedDomain.Entities.Users
{
    public class Member : IdentityUser<Guid>
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public string FullName { get; set; }
        public string IdentificationCard { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string Education { get; set; }
        public DateTime DOB { get; set; }


        public ICollection<ActivityWorker> Activities { get; set; }
    }
}
