using Newtonsoft.Json;

namespace Service.FarmScheduling.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string FullName { get; set; }
    }
}
