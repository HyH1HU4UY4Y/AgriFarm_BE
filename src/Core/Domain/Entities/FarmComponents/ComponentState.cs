using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.Schedules;

namespace SharedDomain.Entities.FarmComponents
{
    public class ComponentState : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }

        public Guid? ActivityId { get; set; }
        public Activity? Activity { get; set; }

        [MaxLength(8000)]
        public string Data { get; private set; }

        public void SetData(Dictionary<string, object> data)
        {
            Data = JsonConvert.SerializeObject(data);
        }

        public Dictionary<string, object>? LoadData()
        {
            if(string.IsNullOrWhiteSpace(Data)) return default;
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(Data);

        }
    }
}
