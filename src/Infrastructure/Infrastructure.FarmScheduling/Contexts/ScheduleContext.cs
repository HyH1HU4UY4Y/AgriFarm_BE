using Infrastructure.FarmScheduling.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Training;
using SharedDomain.Entities.Users;
using SharedApplication.Persistence.Configs;

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
        public DbSet<AdditionOfActivity> Additions { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<MinimalUserInfo> Participants { get; set; }
        public DbSet<TrainingDetail> TrainingDetails { get; set; }
        //public DbSet<HarvestDetail> HarvestDetails { get; set; }
        public DbSet<UsingDetail> UsingDetails { get; set; }
        public DbSet<TreatmentDetail> TreatmentDetails { get; set; }
        public DbSet<AssessmentDetail> AssessmentDetails { get; set; }
        public DbSet<BaseComponent> Components { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeasonConfig).Assembly);
            modelBuilder.Ignore<Site>();
            modelBuilder.Ignore<ExpertInfo>();
            modelBuilder.Ignore<TrainingContent>();
            ReduceComponent(modelBuilder);
            


        }

        private void ReduceComponent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FarmFertilize>().ExtractFertilize();
            modelBuilder.Entity<FarmPesticide>().ExtractPesticide();
            modelBuilder.Entity<FarmSeed>().ExtractSeed();
            modelBuilder.Entity<FarmEquipment>().ExtractEquipment();
            modelBuilder.Entity<FarmWater>().ExtractWater();
            modelBuilder.Entity<FarmSoil>().ExtractSoil();
            //modelBuilder.Entity<FarmProduct>().ExtractProduct();

        }
    }
}
