using Infrastructure.Supply.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Supply.Contexts
{
    public class SupplyContext : MultiSiteDbContext
    {
        public SupplyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplyDetail> SupplyDetails { get; set; }
        //public DbSet<Site> Farm  { get; set; }
        public DbSet<BaseComponent> Components  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);

            modelBuilder.Entity<BaseComponent>().UseTphMappingStrategy();

            modelBuilder.Ignore<Site>();
            modelBuilder.Ignore<ComponentProperty>();
            modelBuilder.Ignore<ComponentState>();
            modelBuilder.Ignore<ComponentDocument>();
        }
    }
}
