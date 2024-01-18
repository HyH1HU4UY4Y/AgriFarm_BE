using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Users
{
    public class MinimalUserInfo: BaseEntity
    {
        public Guid? SiteId { get; set; }
        public Site? Site { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? AvatarImg { get; set; }

    }
}
