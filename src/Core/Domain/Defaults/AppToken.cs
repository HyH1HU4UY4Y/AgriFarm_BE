using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Defaults
{
    public static class AppToken
    {
        public static readonly string Key = "@Super-Secret//Ke.Y369012";
        public static readonly string Issuer = "GAgri";
        public static readonly string Audience = "Everyone";
        public static readonly long ExpiredIn = 60;
    }
}
