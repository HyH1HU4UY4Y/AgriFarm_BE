using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SharedDomain.Entities.Base;
using System.Linq.Expressions;

namespace SharedDomain.Repositories.Base
{
    public interface ISQLRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : IBaseEntity<Guid>
    {

        Task<TEntity?> GetOne(
            Expression<Func<TEntity, bool>> selector,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool checkRole = false);

        Task<List<TEntity>?> GetMany(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                bool checkRole = false);

        Task<TEntity> AddAsync(TEntity entity);
        Task SoftDeleteAsync(TEntity entity);
        Task RawDeleteAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);
        Task AddBatchAsync(IEnumerable<TEntity> entities);
        Task UpdateBatchAsync(IEnumerable<TEntity> entities);
        Guid GetSiteId();
    }


}