using SharedDomain.Assessment;
using SharedDomain.Base;

namespace SharedDomain.Cultivations
{
    public class Activity : BaseEntity, ITraceableItem
    {
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public Guid RiskSurveyId { get; set; }
        public RiskSurvey RiskSurvey { get; set; }



        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }

        public ICollection<ActivityDetail> Details { get; set; }
        public ICollection<ActivityWorker> Workers { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
