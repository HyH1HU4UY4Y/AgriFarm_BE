using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Training
{
    public class TrainingContent : BaseEntity
    {
        

        public string Content { get; set; }
        public string Resource { get; set; }
    }
}
