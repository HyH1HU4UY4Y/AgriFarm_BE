using Infrastructure.Seed.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seed.Contexts
{
    public class SeedlingContext : MultiSiteDbContext
    {
        public SeedlingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FarmSeed> FarmSeeds { get; set; }
        public DbSet<ReferencedSeed> RefSeedInfos { get; set; }
        public DbSet<Site> Sites { get; set; }
        //public DbSet<Activity> Activities { get; set; }
        public DbSet<ComponentProperty> Properties { get; set; }
        //public DbSet<ComponentState> States { get; set; }
        public DbSet<ConsumeCultivation> UsedRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FarmSeed>().ToTable(nameof(FarmSeeds));
            modelBuilder.Entity<ReferencedSeed>().ToTable(nameof(RefSeedInfos));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.Ignore<Activity>();
            modelBuilder.Ignore<ComponentDocument>();
            modelBuilder.Ignore<ComponentState>();

            modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();
            //modelBuilder.Entity<BaseComponent>().UseTpcMappingStrategy();
            //modelBuilder.Ignore<BaseComponent>();
        }
    }
}
