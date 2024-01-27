using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseInfo : BaseEntity
    {

        public string DiseaseName { get; set; }
        [StringLength(8000)]
        public string Symptoms { get; set; }
        [StringLength(8000)]
        public string Cause { get; set; }
        public string PreventiveMeasures { get; set; }
        public string Suggest { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }



    }
}
