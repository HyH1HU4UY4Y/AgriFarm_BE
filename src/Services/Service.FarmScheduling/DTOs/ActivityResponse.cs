using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedDomain.Entities.Schedules;

namespace Service.FarmScheduling.DTOs
{
    public class ActivityResponse
    {
        public Guid Id { get; set; }
        public SeasonResponse Season { get; set; }
        public LandResponse Location { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        [JsonProperty("descriptions")]
        public List<NoteItem> Notes { get; set; }
        public List<UserResponse> Inspectors { get; set; } = new();
        public List<UserResponse> Workers { get; set; } = new();
        public bool IsCompleted { get; set; }
        [JsonProperty("start")]
        public DateTime? StartIn { get; set; }
        [JsonProperty("end")]
        public DateTime? EndIn { get; set; }
        public AdditionResponse Addition { get; set; }
    }
}
