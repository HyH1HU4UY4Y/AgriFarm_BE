using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.Assessment
{
    public class Section : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Criteria> Items { get; set; }
    }
}
