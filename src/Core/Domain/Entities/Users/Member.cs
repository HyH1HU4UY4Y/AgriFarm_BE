using Microsoft.AspNetCore.Identity;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;

namespace SharedDomain.Entities.Users
{
    public class Member : IdentityUser<Guid>
    {
        public Guid? SiteId { get; set; }
        public Site? Site { get; set; }
        public string? FullName { get; set; }
        public string? IdentificationCard { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public DateTime? DOB { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<ActivityParticipant> Activities { get; set; }
    }
}
