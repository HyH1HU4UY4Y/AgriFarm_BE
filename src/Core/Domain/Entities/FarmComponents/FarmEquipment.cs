using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmEquipment: BaseComponent
    {
        public FarmEquipment() {
            Unit = "item";
        }
        public decimal? UnitPrice { get; set; }

        public DateTime? ExpiredIn { get; set; }
    }
}
