using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Training
{
    public class ExpertInfo : BaseEntity, IMultiSite
    {
        public string? FullName { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        public string? ExpertField { get; set; }
        public Guid SiteId { get; set; }

        private string _ceritficates;
        [MaxLength(int.MaxValue)]
        public string? Certificate
        {
            get => _ceritficates;
            private set
            {
                _ceritficates = value;

            }
        }

        [NotMapped]
        public List<ExpertCertification> Certificates
        {
            get => !string.IsNullOrWhiteSpace(_ceritficates) ?
                JsonConvert.DeserializeObject<List<ExpertCertification>>(_ceritficates)!
                : new();
            set => _ceritficates = JsonConvert.SerializeObject(value);
        }

        


    }
    public class ExpertCertification
    {
        public string Name { get; set; }
        public string? Reference { get; set; }
    }


}
