using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Assessment
{
    public class Criteria : BaseEntity
    {
        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public string ControlPoints { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }


    }
}
