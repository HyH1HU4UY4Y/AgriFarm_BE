using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;
using SharedDomain.Users;

namespace SharedDomain.Assessment
{
    public class RiskSurvey : BaseEntity, ITraceableItem
    {
        public Guid ManagerId { get; set; }
        public Member Manager { get; set; }
        public Guid ListId { get; set; }
        public RiskList List { get; set; }




        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
