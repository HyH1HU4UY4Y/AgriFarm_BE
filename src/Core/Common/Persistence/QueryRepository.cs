using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence
{
    public class QueryRepository<TDbContext, TEntity>: IQueryRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : BaseEntity
    {
        protected readonly TDbContext _context;

        public ICollection<TEntity> All { get; }

        public QueryRepository(TDbContext context)
        {
            _context = context;
            All = _context.Set<TEntity>().Where(e=>e.IsDeleted == false).ToList();
        }
    }

    public static class QueryExtensions
    {
        public async static Task<IEnumerable<TEntity>> Get<TDbContext, TEntity>
            (this QueryRepository<TDbContext, TEntity> repository)
            where TDbContext : DbContext where TEntity : BaseEntity
        {
            var list = repository.All;

            return list;
        }
    } 
}
