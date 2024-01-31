using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Risk
{
    public class RiskMaster: BaseEntity
    {

        public string? RiskName { get; set;}
        [StringLength(8000)]
        public string? RiskDescription { get; set;}
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }


    }
}
