using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.ChecklistGlobalGAP
{
    public class ChecklistItem : BaseEntity
    {
        public Guid ChecklistMasterId { get; set; }
        public int OrderNo { get; set; }
        public string? AfNum { get; set; }
        public string? Title { get; set; }
        public string? LevelRoute { get; set; }
        public string? Content { get; set; }
        public bool IsResponse { get; set; } = true;
        public ChecklistMaster? ChecklistMaster { get; set; }
        public ICollection<ChecklistItemResponse>? ChecklistItemResponses { get; set; }
    }
}
