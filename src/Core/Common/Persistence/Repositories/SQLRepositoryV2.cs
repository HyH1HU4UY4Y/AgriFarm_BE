using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SharedApplication.Persistence.Repositories
{
    public class SQLRepositoryV2<TDbContext, TEntity>
        where TDbContext : DbContext where TEntity : class, IBaseEntity<Guid>
    {
        protected readonly TDbContext _context;
        private readonly IQueryable<TEntity> _all;
        private readonly ILogger<SQLRepositoryV2<TDbContext, TEntity>> _logger;

        public SQLRepositoryV2(TDbContext context, ILogger<SQLRepositoryV2<TDbContext, TEntity>> logger)
        {
            _context = context;
            _all = context.Set<TEntity>();
            if (typeof(TEntity).IsAssignableFrom(typeof(ITraceableItem)))
            {
                _all = _all.Where(e => !((ITraceableItem)e).IsDeleted);
            }
            _logger = logger;
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);


            return entity;
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await Task.CompletedTask;
        }


        public async Task SoftDeleteAsync(TEntity entity)
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ITraceableItem)))
                (entity as ITraceableItem)!.IsDeleted = true;
            else
                _logger.LogWarning($"This item (id:{entity.Id}) only can raw delete.");
            _context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
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
