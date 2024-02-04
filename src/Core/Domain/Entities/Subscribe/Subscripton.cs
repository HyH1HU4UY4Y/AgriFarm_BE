using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;

namespace SharedDomain.Entities.Subscribe
{
    public class Subscripton : BaseEntity, IMultiSite
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public Guid SolutionId { get; set; }
        public PackageSolution Solution { get; set; }

        public decimal Price { get; set; }

        public DateTime StartIn { get; set; } = DateTime.Now;
        public DateTime EndIn { get; set; }

    }
}
