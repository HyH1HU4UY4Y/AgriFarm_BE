using Infrastructure.Equipment.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.Schedules;

namespace Infrastructure.Equipment.Contexts
{
    public class FarmEquipmentContext : MultiSiteDbContext
    {
        public FarmEquipmentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FarmEquipment> FarmEquipments { get; set; }
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
            //modelBuilder.Entity<BaseComponent>().UseTpcMappingStrategy();


            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActivityConfig).Assembly);
            modelBuilder.Entity<FarmEquipment>().ToTable(nameof(FarmEquipments));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.Ignore<ComponentDocument>();
            modelBuilder.Ignore<ComponentState>();
            modelBuilder.Ignore<Activity>();
        }
    }
}
