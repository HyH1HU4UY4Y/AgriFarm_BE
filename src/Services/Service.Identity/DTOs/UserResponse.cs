using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        //[JsonProperty("first_name")]
        public string FirstName { get; set; }
        //[JsonProperty("last_name")]
        public string LastName { get; set; }
        //[JsonProperty("avatar")]
        public string? AvatarImg { get; set; }
        //[JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //[JsonProperty("role_name")]
        public string Role { get; set; }

    }
}
