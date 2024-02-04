using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Common
{
    public class PesticideInfo : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? Notes { get; set; }
        public string? Resources { get; set; }
        public ICollection<FarmPesticide> InUse { get; set; }

    }
}
