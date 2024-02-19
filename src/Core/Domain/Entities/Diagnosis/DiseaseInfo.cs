using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedDomain.Entities.Diagnosis
{
    public class DiseaseInfo : BaseEntity
    {

        public string DiseaseName { get; set; }
        [StringLength(Int32.MaxValue)]
        public string Symptoms { get; set; }
        [StringLength(Int32.MaxValue)]
        public string Cause { get; set; }
        [StringLength(Int32.MaxValue)]
        public string PreventiveMeasures { get; set; }
        [StringLength(Int32.MaxValue)]
        public string Suggest { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }



    }
}
