using Microsoft.AspNetCore.Identity;
using SharedDomain.Defaults;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;

namespace SharedDomain.Entities.Users
{
    public class Member : IdentityUser<Guid>, ITraceableItem
    {
        public Guid? SiteId { get; set; }
        public Site? Site { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentificationCard { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public DateTime? DOB { get; set; }

        public string? AvatarImg { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<ActivityParticipant> Activities { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
    }
}
