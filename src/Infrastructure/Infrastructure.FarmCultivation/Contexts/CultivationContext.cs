using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using Infrastructure.FarmCultivation.Config;
using SharedApplication.Persistence.Configs;
using SharedDomain.Entities.FarmComponents.Others;

namespace Infrastructure.FarmCultivation.Contexts
{
    public class CultivationContext : MultiSiteDbContext
    {
        public CultivationContext(DbContextOptions options) : base(options)
        {
        }


        //public DbSet<Site> Sites { get; set; }
        public DbSet<CultivationSeason> Seasons { get; set; }
        //public DbSet<Activity> Activities { get; set; }
        public DbSet<HarvestProduct> Products { get; set; }
        public DbSet<FarmSoil> Locations { get; set; }
        public DbSet<FarmSeed> Seeds { get; set; }
        //public DbSet<AdditionOfActivity> Additions { get; set; }
        //public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        //public DbSet<BaseComponent> Components { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeedConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LandConfig).Assembly);

            modelBuilder.Entity<BaseComponent>()
                .UseTpcMappingStrategy()
                .ExtractComponent();
            modelBuilder.Ignore<Site>();
            modelBuilder.Ignore<AdditionOfActivity>();
            modelBuilder.Ignore<ComponentDocument>();
            modelBuilder.Ignore<Activity>();
            //modelBuilder.Ignore<BaseComponent>();
            modelBuilder.Ignore<ComponentProperty>();
            modelBuilder.Ignore<ComponentState>();

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);


        }
    }
}
