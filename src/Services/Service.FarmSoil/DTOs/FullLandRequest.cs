using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;

namespace Service.Soil.DTOs
{
    public class FullLandRequest
    {
        [Required]
        public LandRequest Land {  get; set; }
        public SupplyContractRequest? Supply {  get; set; }
        public List<PositionPoint> Positions { get; set; }
    }
}
