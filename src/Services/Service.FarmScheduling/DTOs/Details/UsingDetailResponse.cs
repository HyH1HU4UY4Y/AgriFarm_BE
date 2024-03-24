using Newtonsoft.Json;

namespace Service.FarmScheduling.DTOs.Details
{
    public class UsingDetailResponse
    {
        public Guid Id { get; set; }
        [JsonProperty("item")]
        public ComponentResponse Component { get; set; }
        public string UseValue { get; set; }

    }
}
