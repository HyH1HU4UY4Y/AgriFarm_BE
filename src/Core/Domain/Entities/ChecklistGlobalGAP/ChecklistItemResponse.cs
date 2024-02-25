using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.ChecklistGlobalGAP
{
    public class ChecklistItemResponse : BaseEntity
    {
        public Guid ChecklistItemId { get; set; }
        public Guid ChecklistMappingId { get; set; }
        public int Level { get; set; }
        public int Result { get; set; }
        public string? Note { get; set; }
        public string? Attachment { get; set; }
        public ChecklistItem? CheklistItem { get; set; }
        public ChecklistMapping? ChecklistMapping { get; set; } 
    }
}
