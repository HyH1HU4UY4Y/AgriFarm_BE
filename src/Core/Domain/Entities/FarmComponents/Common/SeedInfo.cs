using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Common
{
    public class SeedInfo : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Manufactory { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public string? Details { get; set; }
        public string? Notes { get; set; }
        public string? Resources { get; set; }
        public ICollection<FarmSeed> InUse { get; set; }
    }
}
