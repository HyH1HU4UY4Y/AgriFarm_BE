using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Base
{
    public interface IBaseEntity<out TKey>
    {
        TKey Id { get; }
    }
}
