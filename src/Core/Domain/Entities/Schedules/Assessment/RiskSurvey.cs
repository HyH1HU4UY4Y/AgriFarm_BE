using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.Users;

namespace SharedDomain.Entities.Schedules.Assessment
{
    public class RiskSurvey : AdditionOfActivity
    {

        public Guid ListId { get; set; }
        public RiskList List { get; set; }


        public DateTime? SurveyedAt { get; set; }
        public string Notes { get; set; }



    }
}
