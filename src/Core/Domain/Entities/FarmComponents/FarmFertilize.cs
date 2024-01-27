using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.FarmComponents.Common;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmFertilize : BaseComponent
    {
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; } = 0;
        public Guid? InfoId { get; set; }
        public FertilizeInfo? Info { get; set; }




    }
}
