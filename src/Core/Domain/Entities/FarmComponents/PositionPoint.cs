using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class PositionPoint
    {
        public PositionPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("long")]
        public double Longitude { get; set; }
    }
}
