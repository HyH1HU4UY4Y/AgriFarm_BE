using SharedDomain.Defaults;
using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseDiagnosis : BaseEntity
    {
        public Guid PlantDiseaseId { get; set; }
        public DiseaseInfo? PlantDisease { get; set; }
        public string? Description { get; set; }
        [StringLength(2000)]
        public string? Feedback { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; } = FeedbackStatus.Pending;
        public string? Location { get; set; }
        public Guid? CreateBy { get; set; }
        
        public Guid? LandId { get; set; }

        

    }
}
