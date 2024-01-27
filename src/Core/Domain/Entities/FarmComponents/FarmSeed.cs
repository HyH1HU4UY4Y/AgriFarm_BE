using SharedDomain.Entities.FarmComponents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmSeed : BaseComponent
    {
        public int Stock { get; set; } = 0;
        public decimal? UnitPrice { get; set; }

        public Guid? InfoId { get; set; }
        public SeedInfo? Info { get; set; }

    }
}
