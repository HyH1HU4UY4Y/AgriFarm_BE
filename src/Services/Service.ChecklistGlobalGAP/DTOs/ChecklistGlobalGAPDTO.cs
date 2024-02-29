using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Service.ChecklistGlobalGAP.DTOs
{
    public class ChecklistMasterDTO : ChecklistMaster { }

    public class ChecklistMappingDTO : ChecklistMapping { }

    public class ChecklistItemResponseDTO : ChecklistItemResponse { }
    public class IChecklistItem
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public int OrderNo { get; set; }
        public string? AfNum { get; set; }
        public string? Title { get; set; }
        public string? LevelRoute { get; set; }
        public string? Content { get; set; }
        public bool IsResponse { get; set; }
        public IChecklistItemResponse? ChecklistItemResponse { get; set; }
    }

    public class IChecklistItemResponse
    {
        public int Level { get; set; }
        public int Result { get; set; }
        public string? Note { get; set; }
        public string? Attachment { get; set; }
    }

    public class ChecklistContentDTO 
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Version { get; set; }
        public List<IChecklistItem>? ChecklistItems { get; set; }
    }
}
