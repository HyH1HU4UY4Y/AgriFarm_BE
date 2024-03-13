using SharedDomain.Entities.Base;

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
    }
}
