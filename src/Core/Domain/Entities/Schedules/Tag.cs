using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules
{
    public class Tag : BaseEntity
    {
        public string Title { get; set; }
        //public string Description { get; set; } = "";
        public ICollection<Activity> Activities { get; set; }
    }
}
