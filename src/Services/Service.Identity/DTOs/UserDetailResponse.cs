using Newtonsoft.Json;
using SharedDomain.Defaults;

namespace Service.Identity.DTOs
{
    public class UserDetailResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonProperty("avatar")]
        public string? AvatarImg { get; set; }
        public string? IdentificationCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public string Role { get; set; }
        public string DOB { get; set; }
        [JsonProperty("onboarding")]
        public string CreatedDate { get; set; }
        public List<CertificateResponse> Certificates { get; set; }
    }
}
