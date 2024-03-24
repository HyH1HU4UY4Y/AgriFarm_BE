using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Additions
{
    public class TreatmentDetail: AdditionOfActivity
    {
        public TreatmentDetail() {
            AdditionType = Defaults.AdditionType.Treatment;
        }
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        [MaxLength(int.MaxValue)]
        public string TreatmentDescription {  get; set; }
        
    }
}
