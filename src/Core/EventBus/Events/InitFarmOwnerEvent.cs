using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class InitFarmOwnerEvent
    {
        public InitFarmOwnerEvent(Guid siteId, string? siteName, string? siteCode, string? firstName, 
            string? lastName, string? userName, string email, string address, string? phoneNumber)
        {
            SiteId = siteId;
            SiteName = siteName;
            SiteCode = siteCode;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Guid SiteId { get; set; }
        public string? SiteName { get; set; }
        public string? SiteCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        
    }
}
