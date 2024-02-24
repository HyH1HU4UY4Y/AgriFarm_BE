using Newtonsoft.Json;

namespace Service.FarmSite.DTOs
{
    public class SiteResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SiteCode { get; set; }
        public bool IsActive { get; set; }
        [JsonProperty("avatar")]
        public string? AvatarImg { get; set; }
        [JsonProperty("logo")]
        public string? LogoImg { get; set; }
    }
}
