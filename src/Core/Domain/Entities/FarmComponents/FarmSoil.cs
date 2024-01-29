using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmSoil : BaseComponent
    {
        private string _positionStr;
        public string Position { 
            get => _positionStr;
            set {
                _positionStr = value;

            } 
        }

        [NotMapped]
        public Dictionary<double, double> Positions 
        {
            get => JsonConvert.DeserializeObject<Dictionary<double, double>>(_positionStr)??new();
            set => _positionStr = JsonConvert.SerializeObject(value);
        }

        public double Acreage { get; set; }

    }
}
