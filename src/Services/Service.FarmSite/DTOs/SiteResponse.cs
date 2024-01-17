namespace Service.FarmSite.DTOs
{
    public class SiteResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Intro { get; set; }
        public string SiteKey { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
