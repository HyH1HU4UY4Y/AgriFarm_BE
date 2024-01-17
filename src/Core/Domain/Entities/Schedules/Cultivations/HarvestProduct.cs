using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Cultivations
{
    public class HarvestProduct : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? HarvestTime { get; set; }
        public double? Quantity { get; set; } = 0;
        public string Unit { get; set; }
        public string Traceability { get; private set; }


        public Guid SeedId { get; set; }
        public FarmSeed Seed { get; set; }

        public Guid LandId { get; set; }
        public FarmSoil Land { get; set; }

        public Guid SeasonId { get; set; }
        public CultivationSeason Season { get; set; }

        public Guid FarmId { get; set; }
        public Site Farm { get; set; }

        public void SetTraceability((string provider, string type, string data) traceability)
            => JsonConvert.SerializeObject(traceability);

        public string GetTraceData()
            => JsonConvert.DeserializeObject<(string provider, string type, string data)>(Traceability).data;
    }
}
