using SharedDomain.Entities.Base;
using SharedDomain.Entities.Diagnosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Risk
{
    public class RiskItem: BaseEntity
    {
        public Guid RiskMasterId { get; set; }
        [JsonIgnore]
        public RiskMaster? RiskMaster { get; set; }
        public string? RiskItemTitle { get; set; }
        public string? RiskItemDiv { get; set; }
        public string? RiskItemType { get; set; }
        public string? Must { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }

        public ICollection<RiskItemContent>? RiskItemContents { get; set; }
    }
}
