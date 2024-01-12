using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Repositories.Base
{
    public interface IQueryRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : IBaseEntity<Guid>
    {
        ICollection<TEntity> All { get;  }
    }
}
