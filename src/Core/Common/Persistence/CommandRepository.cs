using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence
{
    public class CommandRepository<TDbContext, TEntity> : ICommandRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : BaseEntity
    {
        protected readonly TDbContext _context;

        public CommandRepository(TDbContext context)
        {
            _context = context;
        }

        // Insert
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
