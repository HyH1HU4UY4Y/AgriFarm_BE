using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Risk
{
    public class RiskMapping : BaseEntity
    {
        public Guid RiskMasterId { get; set; }
        public Guid TaskId { get; set; }
        public RiskMaster? RiskMaster { get; set; }
    }
}
