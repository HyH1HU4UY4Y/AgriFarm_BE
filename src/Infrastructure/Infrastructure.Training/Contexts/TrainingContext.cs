using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Training.Contexts
{
    public class TrainingContext : MultiSiteDbContext
    {
        public TrainingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TrainingDetail> Trainings { get; set; }
        public DbSet<TrainingContent> Contents { get; set; }
        public DbSet<ExpertInfo> ExpertInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Activity>();
            modelBuilder.Ignore<Site>();
        }
    }
}
