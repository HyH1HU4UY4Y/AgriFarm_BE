using System.ComponentModel.DataAnnotations;

namespace Service.ChecklistGlobalGAP.DTOs
{
    public class ChecklistGlobalGAPGetListRequest
    {
        public int perPage { get; set; }
        public int pageId { get; set; }
        [Required]
        public Guid userId { get; set; }
    }
    public class ChecklistGlobalGAPAddListRequest
    {
        [Required]
        public Guid userId { get; set; }
        [Required]
        public Guid checklistMasterId { get; set; }
    }
    public class ChecklistItemDef
    {
        public int orderNo { get; set; }
        public string? afNum { get; set; }
        public string? title { get; set; }
        public string? levelRoute { get; set; }
        public string? content { get; set; }
        public bool isResponse { get; set; }
    }
    public class ChecklistGlobalGAPCreateRequest
    {
        public string name { get; set; } = "No Name";
        public bool isDraft { get; set; } = true;
        public List<ChecklistItemDef>? checklistItems { get; set; }
    }
    public class ChecklistItemResponseContent
    {
        public Guid checklistItemId { get; set; }
        public int level { get; set; }
        public int result { get; set; }
        public string? note { get; set; }
        public string? attachment { get; set; }
    }
    public class ChecklistGlobalGAPCreateResponseRequest
    {
        public Guid checklistMappingId { get; set; }
        public List<ChecklistItemResponseContent>? checklistItemResponses { get; set; }
    }
}
