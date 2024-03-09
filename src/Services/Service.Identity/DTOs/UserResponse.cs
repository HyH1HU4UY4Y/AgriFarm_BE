using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonProperty("avatar")]
        public string? AvatarImg { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string? Address { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool isLockout { get; set; }

    }
}
