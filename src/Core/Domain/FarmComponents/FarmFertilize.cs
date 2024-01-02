using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.FarmComponents
{
    public class FarmFertilize : BaseComponent
    {
        public string NRatio { get; set; }
        public string PRatio { get; set; }
        public string KRatio { get; set; }
        public int Stock { get; set; }
        public Guid? InfoId { get; set; }
        public FertilizeInfo? Info { get; set; }




    }
}
