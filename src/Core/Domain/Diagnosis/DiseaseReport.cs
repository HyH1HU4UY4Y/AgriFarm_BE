using SharedDomain.Base;

namespace SharedDomain.Diagnosis
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
