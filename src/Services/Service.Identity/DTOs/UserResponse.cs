namespace Service.Identity.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarImg { get; set; }

    }
}
