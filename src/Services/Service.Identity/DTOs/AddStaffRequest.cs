using System.ComponentModel.DataAnnotations;

namespace Service.Identity.DTOs
{
    public class AddStaffRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Password not match.")]
        public string ConfirmPassword { get; set; }
    }
}
