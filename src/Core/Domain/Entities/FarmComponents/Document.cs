using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents
{
    public class Document : BaseEntity, ITraceableItem
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Resource { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public ICollection<ComponentDocument> Components { get; set; }

    }
}
