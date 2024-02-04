using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.GAPs
{
    public class CheckList : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Notes { get; set; }

        public ICollection<Section> Sections { get; set; }
    }
}
