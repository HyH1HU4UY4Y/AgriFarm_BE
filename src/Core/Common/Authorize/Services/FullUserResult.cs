using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Authorize.Services
{
    public class FullUserResult
    {
        public Member Info { get; set; }
        public List<string> Roles { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
