using SharedDomain.Entities.FarmComponents;

namespace Service.Soil.DTOs
{
    public class LandResponse
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Acreage { get; set; }
        public List<PositionPoint> Positions { get; set; }
    }
}
