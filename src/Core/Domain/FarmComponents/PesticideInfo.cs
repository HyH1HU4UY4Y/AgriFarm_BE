using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.FarmComponents
{
    public class PesticideInfo : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Resources { get; set; }
        public ICollection<FarmPesticide> Uses { get; set; }

    }
}
