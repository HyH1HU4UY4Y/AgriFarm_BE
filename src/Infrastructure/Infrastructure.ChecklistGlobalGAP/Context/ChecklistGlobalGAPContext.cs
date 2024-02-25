using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Infrastructure.ChecklistGlobalGAP.Context
{
    public class ChecklistGlobalGAPContext : BaseDbContext
    {
        public ChecklistGlobalGAPContext()
        {
        }

        public ChecklistGlobalGAPContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ChecklistMaster> ChecklistMasters { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<ChecklistItemResponse> ChecklistItemResponses { get; set; }
        public DbSet<ChecklistMapping> ChecklistMapping { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
