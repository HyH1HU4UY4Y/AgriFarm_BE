using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskItemContent: BaseEntity
    {
        [JsonIgnore]
        public Guid RiskItemId { get; set; }
        [JsonIgnore]
        public RiskItem? RiskItem { get; set; }

        public string? RiskItemContentTitle { get; set; }

        public int? OrderBy { get; set; }
        [StringLength(8000)]
        public string? Anwser { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }


    }
}
