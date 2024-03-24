using Newtonsoft.Json;

namespace Service.FarmScheduling.DTOs
{
    public class AdditionResponse
    {
        public Guid Id { get; set; }
        [JsonProperty("type")]
        public string AdditionType { get; set; }
        ///public Dictionary<string, object> Data { get; set; } = new();
    }
}
