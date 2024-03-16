using SharedDomain.Entities.Base;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskMaster: BaseEntity
    {
        public string? RiskName { get; set; }
        public string? RiskDescription { get; set; }
        public bool IsDraft { get; set; } = true;
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public ICollection<RiskItem>? RiskItems { get; set; }
        [JsonIgnore]
        public ICollection<RiskMapping>? RiskMappings { get; set; }
    }
}
