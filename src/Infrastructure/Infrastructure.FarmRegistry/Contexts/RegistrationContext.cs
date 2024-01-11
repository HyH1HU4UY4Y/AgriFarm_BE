using SharedApplication.Persistence;
using Infrastructure.Registration.Config;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Subscribe;
using Application.CommonExtensions;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;

namespace Infrastructure.FarmRegistry.Contexts
{
    public class RegistrationContext : BaseDbContext
    {
        public RegistrationContext(DbContextOptions options) : base(options)
        {
        }

        protected RegistrationContext()
        {
        }

        public DbSet<PackageSolution> PackageSolutions { get; set; }
        public DbSet<FarmRegistration> FarmRegistrations { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<Subscripton>();
            //modelBuilder.Ignore<BaseComponent>();

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmRegistrationConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PackageSolutionConfig).Assembly);
            //modelBuilder.SetQueryFilterOnAllEntities<ITraceableItem>(e => !e.IsDeleted);

        }
    }
}
