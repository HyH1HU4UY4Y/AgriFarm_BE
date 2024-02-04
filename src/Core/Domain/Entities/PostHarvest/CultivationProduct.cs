using SharedDomain.Entities.Base;
using SharedDomain.Entities.Cultivations;

namespace SharedDomain.Entities.PostHarvest
{
    public class CultivationProduct : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid SeasonId { get; set; }
        public CultivationSeason Season { get; set; }

        public double Amount { get; set; }
        public string Unit { get; set; }
        public DateTime? HarvestTime { get; set; } = DateTime.Now;

        public ICollection<ProductTraceability> Traceabilities { get; set; }



    }
}
