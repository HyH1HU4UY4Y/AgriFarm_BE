using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;

namespace SharedDomain.Entities.Assessment
{
    public class GAPSurvey : BaseEntity, ITraceableItem
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public Guid ListId { get; set; }
        public CheckList List { get; set; }

        public string Info { get; set; }
        public string Notes { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<GAPSurveyDetail> Items { get; set; }
    }
}
