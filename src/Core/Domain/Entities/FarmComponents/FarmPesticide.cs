using SharedDomain.Entities.FarmComponents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmPesticide : BaseComponent
    {
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; }

        public Guid? InfoId { get; set; }
        public PesticideInfo? Info { get; set; }
    }
}
