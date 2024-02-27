using SharedDomain.Entities.FarmComponents.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmPesticide : BaseComponent
    {
        public FarmPesticide()
        {
            IsConsumable = true;
        }
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; } = 0;
        public Guid? ReferenceId { get; set; }
        public ReferencedPesticide? Reference { get; set; }
    }
}
