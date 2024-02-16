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
using SharedApplication.MultiTenant.Implement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SharedApplication.Persistence.Repositories
{
    public class SQLRepositoryV2<TDbContext, TEntity> : ISQLRepository<TDbContext, TEntity> 
        where TDbContext : DbContext where TEntity : class, IBaseEntity<Guid>
    {
        protected readonly TDbContext _context;
        private readonly IQueryable<TEntity> _all;
        private readonly ILogger<SQLRepositoryV2<TDbContext, TEntity>> _logger;
        private readonly IMultiTenantResolver _multiTenantResolver;
        private Guid _siteId = Guid.Empty;

        public SQLRepositoryV2(TDbContext context,
            ILogger<SQLRepositoryV2<TDbContext, TEntity>> logger,
            IMultiTenantResolver multiTenantResolver)
        {
            _context = context;
            _all = context.Set<TEntity>();
            if (typeof(TEntity).IsAssignableTo(typeof(ITraceableItem)))
            {
                _all = _all.Where(e => !((ITraceableItem)e).IsDeleted);
            }
            _logger = logger;
            _multiTenantResolver = multiTenantResolver;
            if (!_multiTenantResolver.IsSuperAdmin())
            {
                _siteId = _multiTenantResolver.GetTenantId();
            }
        }


        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);


            return entity;
        }


        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return true;
        }


        public virtual async Task SoftDeleteAsync(TEntity entity)
        {
            if (typeof(TEntity).IsAssignableTo(typeof(ITraceableItem)))
                (entity as ITraceableItem)!.IsDeleted = true;
            else
                _logger.LogWarning($"This item (id:{entity.Id}) only can raw delete.");
            _context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity?> GetOne(
            Expression<Func<TEntity, bool>> selector,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool checkRole = false)
        {
            var rs = _all;
            if (include != null) rs = include(rs);
            return await rs.FirstOrDefaultAsync(selector);

        }

        public virtual Task<List<TEntity>?> GetMany(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                bool siteFilter = false)
        {
            var items = _all;


            if (include != null)
            {
                items = include(items);
            }

            if (filter != null)
            {
                items = items.Where(filter);
            }
            if (siteFilter && !_multiTenantResolver.IsSuperAdmin() 
                && items.AsEnumerable().All(e=>e is IMultiSite))
            {
                items = items.AsEnumerable()
                    .Cast<IMultiSite>()
                    .Where(e=>e.SiteId == _siteId)
                    //.Where(e => e.GetType().GetProperty("SiteId").GetValue(e).ToString() == _siteId.ToString())
                    .Cast<TEntity>()
                    .AsQueryable();
            }

            return Task.FromResult(items.AsNoTracking().ToList())!;

        }

        public virtual async Task RawDeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }


        public virtual async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {

            if (await _all.AnyAsync(e => e.Id == entity.Id))
            {
                _context.Set<TEntity>().Add(entity);
            }

            _context.Set<TEntity>().Update(entity);

            return entity;

        }

        public virtual async Task AddBatchAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);

            await Task.CompletedTask;
        }

        public virtual async Task UpdateBatchAsync(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);

            await Task.CompletedTask;
        }

        public Guid GetSiteId()
            => _siteId;

    }
}
