using SharedDomain.Entities.FarmComponents;

namespace Service.Soil.DTOs
{
    public class LandResponse
    {
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }

        public string Name { get; set; }
        public List<PositionPoint> Positions { get; set; }
    }
}
