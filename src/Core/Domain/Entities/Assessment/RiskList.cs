using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Assessment
{
    public class RiskList : BaseEntity, ITraceableItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }


        public Guid SiteId { get; set; }
        public Site Site { get; set; }


    }
}
