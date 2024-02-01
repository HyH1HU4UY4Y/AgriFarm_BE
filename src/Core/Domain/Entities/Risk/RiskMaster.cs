using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskMaster: BaseEntity
    {

        public string? RiskName { get; set;}
        [StringLength(8000)]
        public string? RiskDescription { get; set;}

        public bool IsDraft { get; set;} = true;
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }

        
        public ICollection<RiskItem>? RiskItems { get; set; }


    }
}
