using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;

namespace SharedDomain.Repositories.Base
{
    public interface ICommandRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : IBaseEntity<Guid>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}