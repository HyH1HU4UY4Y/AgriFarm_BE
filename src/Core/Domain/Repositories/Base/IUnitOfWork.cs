using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;

namespace SharedDomain.Repositories.Base
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}