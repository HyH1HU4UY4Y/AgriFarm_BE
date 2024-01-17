using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Commands
{
    public class CreateMemberCommand
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


}
