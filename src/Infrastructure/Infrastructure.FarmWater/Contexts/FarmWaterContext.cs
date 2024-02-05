using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Water.Contexts
{
    public class FarmWaterContext : MultiSiteDbContext
    {
        public FarmWaterContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FarmSoil> FarmLands { get; set; }
        public DbSet<Site> Sites { get; set; }
        //public DbSet<Activity> Activities { get; set; }
        public DbSet<ComponentProperty> Properties { get; set; }
        public DbSet<ComponentState> States { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmSoilConfig).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActivityConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);*/

            modelBuilder.Ignore<ComponentDocument>();
            modelBuilder.Ignore<Activity>();


            modelBuilder.Entity<BaseComponent>().UseTpcMappingStrategy();
            modelBuilder.Ignore<BaseComponent>();
        }

    }
}
