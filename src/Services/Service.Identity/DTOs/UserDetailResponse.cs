using Newtonsoft.Json;
using SharedDomain.Defaults;

namespace Service.Identity.DTOs
{
    public class UserDetailResponse
    {
        public Guid Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("Avatar")]
        public string? AvatarImg { get; set; }
        public string? IdentificationCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public string DOB { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set;}
        public List<CertificateResponse> Certificates { get; set; }
    }
}
