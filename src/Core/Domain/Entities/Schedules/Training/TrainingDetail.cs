using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Training
{
    public class TrainingDetail : AdditionOfActivity
    {
        public Guid ExpertId { get; set; }
        public ExpertInfo Expert { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TrainingContent> Contents { get; set; }
    }
}
