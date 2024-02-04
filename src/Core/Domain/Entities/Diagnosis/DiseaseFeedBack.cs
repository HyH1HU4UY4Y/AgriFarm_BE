using SharedDomain.Entities.Base;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseFeedBack : BaseEntity, ITraceableItem
    {
        public Guid UserId { get; set; }
        public Member User { get; set; }
        public Guid DiagnosisId { get; set; }
        public DiseaseDiagnosis Diagnosis { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
