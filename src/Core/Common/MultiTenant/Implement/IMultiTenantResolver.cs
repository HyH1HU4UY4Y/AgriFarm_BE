using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.MultiTenant.Implement
{
    public interface IMultiTenantResolver
    {
        string GetTenantIdAsync();
    }
}
