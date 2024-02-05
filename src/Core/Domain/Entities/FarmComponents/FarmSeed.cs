using SharedDomain.Entities.FarmComponents.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmSeed : BaseComponent
    {
        public FarmSeed() {
            IsConsumable = true;
        }
        public int Stock { get; set; } = 0;
        public decimal? UnitPrice { get; set; }
        public Guid? ReferenceId { get; set; }
        public ReferencedSeed? Reference { get; set; }

    }
}
