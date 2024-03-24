using Newtonsoft.Json;

namespace Service.FarmScheduling.DTOs.Details
{
    public class TreatmentDetailResponse
    {

        [JsonProperty("item")]
        public ComponentResponse Component { get; set; }
        [JsonProperty("method")]
        public string TreatmentDescription { get; set; }
    }
}
