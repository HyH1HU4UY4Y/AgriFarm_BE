using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class InitFarmOwnerEvent
    {
        public InitFarmOwnerEvent(Guid siteId, string? fullName,
            string? userName, string email, 
            string address, string phoneNumber = null)
        {
            SiteId = siteId;
            FullName = fullName;
            UserName = userName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public Guid SiteId { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
