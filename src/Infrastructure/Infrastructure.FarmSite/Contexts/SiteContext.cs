using Infrastructure.FarmSite.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.MultiTenant.Implement;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmSite.Contexts
{
    public class SiteContext : MultiSiteDbContext
    {
        public SiteContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<CapitalState> CapitalStates { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ComponentDocument> ComponentDocuments { get; set; }
        public DbSet<Subscripton> SubscriptonBills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<BaseComponent>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillConfig).Assembly);
        }
    }
}
