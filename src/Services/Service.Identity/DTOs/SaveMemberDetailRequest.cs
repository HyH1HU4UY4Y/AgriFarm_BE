using SharedDomain.Defaults;

namespace Service.Identity.DTOs
{
    public class SaveMemberDetailRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? IdentificationCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public DateTime? DOB { get; set; }
    }
}
