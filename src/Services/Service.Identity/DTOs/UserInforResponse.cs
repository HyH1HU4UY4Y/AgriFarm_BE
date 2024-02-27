using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class UserInforResponse
    {
        
        public string UserName { get; set; }
        
        public string FullName { get; set; }
        
        public string Email { get; set; }
        public string SiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string Role { get; set; }
    }
}
