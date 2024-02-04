using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Common
{
    public class ResourceItem
    {
        public ResourceItem(string key, string uRL, DateTime epiredIn)
        {
            Key = key;
            URL = uRL;
            EpiredIn = epiredIn;
        }

        public string Key { get; set; }
        public string URL { get; set; }
        public DateTime EpiredIn { get; set; } = DateTime.Now;

        public string ToJsonString()
            => JsonConvert.SerializeObject(this);
    }
}
