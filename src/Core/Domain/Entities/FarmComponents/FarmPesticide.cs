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
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; }
        public Guid? ReferenceId { get; set; }
        public ReferencedPesticide? Reference { get; set; }
    }
}
