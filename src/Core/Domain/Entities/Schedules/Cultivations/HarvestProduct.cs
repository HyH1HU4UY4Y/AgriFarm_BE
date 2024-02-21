using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Cultivations
{
    public class HarvestProduct : BaseEntity, IMultiSite
    {
        public string Name { get; set; }
        public DateTime? HarvestTime { get; set; }
        public double? TotalQuantity { get; set; } = 0;
        public double? Quantity { get; set; } = 0;
        public string Unit { get; set; }
        private string _traceability;

        public string? Traceability {
            get => _traceability;
            private set
            {
                _traceability = value;

            }
        }
        [NotMapped]
        public (string? provider, string type, string data) TraceItem 
        { 
            get => !string.IsNullOrWhiteSpace(_traceability) ?
                JsonConvert.DeserializeObject<(string provider, string type, string data)>(_traceability)!
                : new();
            set {
                value.provider = string.IsNullOrWhiteSpace(value.provider) ? "none": value.provider;
                _traceability = JsonConvert.SerializeObject(value);
            } 
        }


        public Guid SeedId { get; set; }
        public FarmSeed Seed { get; set; }

        public Guid LandId { get; set; }
        public FarmSoil Land { get; set; }

        public Guid SeasonId { get; set; }
        public CultivationSeason Season { get; set; }

        public Guid SiteId { get; set; }
        public Site Site { get; set; }

    }
}
