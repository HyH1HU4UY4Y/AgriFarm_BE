using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmCultivation.Contexts
{
    public class CultivationContext : MultiSiteDbContext
    {
        public CultivationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CultivationSeason> Seasons { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<HarvestProduct> Products { get; set; }
        public DbSet<FarmSoil> Locations { get; set; }
        public DbSet<FarmSeed> Seeds { get; set; }
        public DbSet<AdditionOfActivity> Additions { get; set; }
        //public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<BaseComponent> Components { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();
/*
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeasonConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmSoil).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpertConfig).Assembly);
*/

        }
    }
}
