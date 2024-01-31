using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Risk
{
    public class RiskItemContent: BaseEntity
    {
        public Guid RiskItemId { get; set; }
        public RiskItem? RiskItem { get; set; }

        public string? RiskItemContentTitle { get; set; }

        public int? OrderBy { get; set; }
        [StringLength(8000)]
        public string? Anwser { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }


    }
}
