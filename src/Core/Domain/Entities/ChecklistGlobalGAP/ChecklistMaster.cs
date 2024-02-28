using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedDomain.Entities.ChecklistGlobalGAP
{
    public class ChecklistMaster : BaseEntity
    {
        public string? Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Version { get; set; }
        public bool isDraft { get; set; } = true;
        public ICollection<ChecklistItem>? ChecklistItems { get; set; }
    }
}
