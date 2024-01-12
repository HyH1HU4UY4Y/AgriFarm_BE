using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Base
{
    public interface IMultiSite
    {
        Guid SiteId { get; set; }
    }
}
