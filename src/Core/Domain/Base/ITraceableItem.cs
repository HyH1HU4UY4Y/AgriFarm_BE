using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Base
{
    public interface ITraceableItem
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastModify { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
