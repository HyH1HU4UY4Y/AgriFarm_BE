using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Schedules
{
    public class Activity : BaseEntity
    {
        public Guid SeasonId { get; set; }
        public CultivationSeason Season { get; set; }

        public Guid LocationId { get; set; }
        public FarmSoil Location { get; set; }

        public Guid SiteId { get; set; }
        public Site Site { get; set; }


        public string Title { get; set; }
        public string? Description { get; set; }

        [StringLength(5000)]
        public string? Notes { get; set; }

        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        public ICollection<AdditionOfActivity> Addtions { get; set; }
        public ICollection<ActivityParticipant> Participants { get; set; }

        public DateTime? StartIn { get; set; }
        public DateTime? EndIn { get; set; }

    }
}
