using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseReport : BaseEntity, ITraceableItem
    {
        public string Title { get; set; }
        public string Content { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }



    }
}
