using SharedDomain.Entities.Base;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskItemContent: BaseEntity
    {
        public Guid RiskItemId { get; set; }
        public Guid RiskMappingId {  get; set; }
        public string? Anwser { get; set; }
        [JsonIgnore]
        public RiskItem? RiskItem { get; set; }
        [JsonIgnore]
        public RiskMapping? RiskMapping { get; set; }
    }
}
