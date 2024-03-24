using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Additions
{
    public class AssessmentDetail: AdditionOfActivity
    {
        public Guid? RiskListId { get; set; }
    }
}
