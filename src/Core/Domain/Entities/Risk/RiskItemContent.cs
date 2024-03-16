using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskItemContent: BaseEntity
    {
        public Guid RiskItemId { get; set; }
        public Guid RiskMappingId {  get; set; }
        [StringLength(Int32.MaxValue)]
        public string? Anwser { get; set; }
        [JsonIgnore]
        public RiskItem? RiskItem { get; set; }
        [JsonIgnore]
        public RiskMapping? RiskMapping { get; set; }
    }
}
