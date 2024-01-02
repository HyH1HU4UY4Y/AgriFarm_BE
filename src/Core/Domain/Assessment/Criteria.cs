using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.Assessment
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
