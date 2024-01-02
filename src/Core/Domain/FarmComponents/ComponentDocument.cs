using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public class ComponentDocument : BaseEntity, ITraceableItem
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
