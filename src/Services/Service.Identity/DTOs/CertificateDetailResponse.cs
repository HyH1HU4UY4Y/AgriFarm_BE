using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class CertificateDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        [JsonProperty("url")]
        public string? Resource { get; set; }
    }
}
