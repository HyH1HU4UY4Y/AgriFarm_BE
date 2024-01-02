using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.FarmComponents
{
    public class FarmWater : BaseComponent
    {
        public string Position { get; set; }
        public string FromSource { get; set; }
        public double Acreage { get; set; }
    }
}
