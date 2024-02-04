using Infrastructure.Fertilize.Config;
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

namespace Infrastructure.Fertilize.Contexts
{
    public class FarmFertilizeContext : MultiSiteDbContext
    {
        public FarmFertilizeContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<FarmFertilize> FarmFertilizes { get; set; }
        public DbSet<FertilizeInfo> FertilizeInfos { get; set; }
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

            modelBuilder.Entity<FertilizeInfo>().ToTable(nameof(FertilizeInfos));
            modelBuilder.Entity<FarmFertilize>().ToTable(nameof(FarmFertilizes));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.Ignore<Activity>();
            modelBuilder.Ignore<ComponentState>();
            modelBuilder.Ignore<ComponentDocument>();

            //modelBuilder.Entity<BaseComponent>().UseTpcMappingStrategy();
            modelBuilder.Entity<AdditionOfActivity>().UseTpcMappingStrategy();
            //modelBuilder.Ignore<BaseComponent>();
        }
    }
}
