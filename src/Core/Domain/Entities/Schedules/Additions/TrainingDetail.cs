using SharedDomain.Entities.Training;

namespace SharedDomain.Entities.Schedules.Additions
{
    public class TrainingDetail : AdditionOfActivity
    {
        public Guid ExpertId { get; set; }
        public ExpertInfo Expert { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid ContentId { get; set; }
        public TrainingContent Content { get; set; }
    }
}
