using Infrastructure.FarmScheduling.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.MultiTenant.Implement;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Assessment;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmScheduling.Contexts
{
    public class ScheduleContext : MultiSiteDbContext
    {
        public ScheduleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CultivationSeason> Seasons { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FarmSoil> Locations { get; set; }
        public DbSet<AdditionOfActivity> Additions { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<MinimalUserInfo> Participants { get; set; }
        public DbSet<TrainingDetail> TrainingDetails { get; set; }
        public DbSet<ConsumeCultivation> ConsumeDetails { get; set; }
        public DbSet<BaseComponent> Components { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();
            modelBuilder.Entity<TrainingDetail>().ToTable(nameof(TrainingDetails));
            modelBuilder.Entity<ConsumeCultivation>().ToTable(nameof(ConsumeDetails));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeasonConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmSoilConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpertConfig).Assembly);


        }
    }
}
