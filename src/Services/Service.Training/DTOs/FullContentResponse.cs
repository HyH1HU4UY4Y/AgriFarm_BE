using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Service.Training.DTOs
{
    public class FullContentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonProperty("refer")]
        public string Resource { get; set; }
    }
}
