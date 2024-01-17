using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SharedApplication.MultiTenant.Implement;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence.Repositories
{
    public class SQLRepository<TDbContext, TEntity> : ISQLRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : BaseEntity
    {
        protected readonly TDbContext _context;
        private readonly IQueryable<TEntity> _all;
        private readonly IMultiTenantResolver _resolver;
        protected readonly string _siteId;

        public SQLRepository(TDbContext context, IMultiTenantResolver resolver)
        {
            _context = context;
            _all = context.Set<TEntity>().Where(e => !e.IsDeleted);
            _resolver = resolver;
            _siteId = _resolver.GetTenantIdAsync();
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task SoftDeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetOne(
            Expression<Func<TEntity, bool>> selector,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var rs = _all;
            if (include != null) rs = include(rs);
            return await rs.FirstOrDefaultAsync(selector);

        }

        public Task<IQueryable<TEntity>?> GetMany(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var items = _all;
            if (filter != null)
            {
                items = items.Where(filter);
            }

            if (include != null)
            {
                items = include(items);
            }

            return Task.FromResult(items)!;

        }
    }
}
