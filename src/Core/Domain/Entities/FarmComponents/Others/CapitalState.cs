using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Others
{
    public class CapitalState : BaseEntity, IMultiSite
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }


    }
}
