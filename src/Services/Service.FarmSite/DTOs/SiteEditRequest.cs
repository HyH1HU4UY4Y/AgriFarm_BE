namespace Service.FarmSite.DTOs
{
    public class SiteEditRequest
    {
        public string Name { get; set; }
        public string? Intro { get; set; } 
        public string? AvatarImg { get; set; }
        public string? LogoImg { get; set; }
    }
}
