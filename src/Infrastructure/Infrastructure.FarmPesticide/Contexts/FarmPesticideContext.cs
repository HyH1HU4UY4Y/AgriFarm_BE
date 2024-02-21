using Infrastructure.Pesticide.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;

namespace Infrastructure.Pesticide.Contexts
{
    public class FarmPesticideContext : MultiSiteDbContext
    {
        public FarmPesticideContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FarmPesticide> FarmPesticides { get; set; }
        public DbSet<ReferencedPesticide> RefPesticideInfos { get; set; }
        public DbSet<Site> Sites { get; set; }
        //public DbSet<Activity> Activities { get; set; }
        public DbSet<ComponentProperty> Properties { get; set; }
        //public DbSet<ComponentState> States { get; set; }
        //public DbSet<ConsumeCultivation> UsedRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReferencedPesticide>().ToTable(nameof(RefPesticideInfos));
            modelBuilder.Entity<FarmPesticide>().ToTable(nameof(FarmPesticides));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.Ignore<Activity>();
            modelBuilder.Ignore<ComponentDocument>();
            modelBuilder.Ignore<ComponentState>();

            //modelBuilder.Entity<BaseComponent>().UseTpcMappingStrategy();
            //modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();
            //modelBuilder.Ignore<BaseComponent>();
        }
    }
}
