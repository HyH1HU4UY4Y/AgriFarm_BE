using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents
{
    public class Document : BaseEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Resource { get; set; }


        public ICollection<ComponentDocument> Components { get; set; }

    }
}
