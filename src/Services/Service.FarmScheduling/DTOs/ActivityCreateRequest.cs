using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using System.ComponentModel.DataAnnotations;

namespace Service.FarmScheduling.DTOs
{
    public class ActivityCreateRequest
    {
        [Required]
        public string SeasonId { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [JsonProperty("type")]
        public string AdditionType { get; set; }
        [MaxLength(5)]
        [JsonProperty("descriptions")]
        public List<NoteItem> Notes { get; set; } = new();
        [MaxLength(1)]
        public List<Guid> Workers { get; set; } = new();
        [MaxLength(1)]
        public List<Guid> Inspectors { get; set; } = new();
        public Dictionary<string, object> Addition { get; set; } = new();
        [MaxLength(2)]
        public DateTime[] Duration { get; set; } = Array.Empty<DateTime>();
    }
}
