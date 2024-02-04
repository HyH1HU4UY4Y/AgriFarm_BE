using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseDiagnosis : BaseEntity, ITraceableItem
    {
        public Guid ReportId { get; set; }
        public DiseaseReport Report { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<DiseaseFeedBack> Diagnoses { get; set; }
    }
}
