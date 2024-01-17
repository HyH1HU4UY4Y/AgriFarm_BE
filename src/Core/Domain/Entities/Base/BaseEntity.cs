using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity<Guid>, ITraceableItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

    }
}
