using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Training
{
    public class ExpertInfo : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public string? ExpertField { get; set; }
        public string? Certificates { get; private set; } = "";
        [NotMapped]
        private List<(string name, string reference)> _ceritficates = null;


        public void AddCertificate((string name, string reference) certificate)
        {
            if (_ceritficates is null)
            {
                try
                {
                    _ceritficates = JsonConvert.DeserializeObject<List<(string name, string reference)>>(Certificates);
                }
                catch { }
                if (_ceritficates is null) _ceritficates = new();
            }
            _ceritficates.Add(certificate);
            Certificates = JsonConvert.SerializeObject(_ceritficates);
        }

        public List<(string name, string reference)> GetCertificates()
        {

            if (_ceritficates is null)
            {
                try
                {
                    _ceritficates = JsonConvert.DeserializeObject<List<(string name, string reference)>>(Certificates)!;
                }
                catch { }
            }
            return _ceritficates ?? new();
        }
    }
}
