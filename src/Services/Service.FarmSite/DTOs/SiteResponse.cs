namespace Service.FarmSite.DTOs
{
    public class SiteResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Intro { get; set; }
        public string SiteCode { get; set; }
        public bool IsActive { get; set; }
        public string? AvatarImg { get; set; }
        public string? LogoImg { get; set; }
    }
}
