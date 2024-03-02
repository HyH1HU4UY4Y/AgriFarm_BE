using SharedDomain.Entities.Base;
using SharedDomain.Defaults;

namespace SharedDomain.Entities.ChecklistGlobalGAP
{
    public class ChecklistMapping : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ChecklistMasterId { get; set; }
        public ChecklistStatus Status { get; set; } = ChecklistStatus.NotYet;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ChecklistMaster? ChecklistMaster { get; set; }
    }
}
