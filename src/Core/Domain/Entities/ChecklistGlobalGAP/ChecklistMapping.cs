using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.ChecklistGlobalGAP
{
    public class ChecklistMapping : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ChecklistMasterId { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ChecklistItemResponse>? ChecklistItemResponses { get; set; }
        public ChecklistMaster ChecklistMaster { get; set; }
    }
}
