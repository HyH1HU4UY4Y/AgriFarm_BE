using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.Risk;

namespace Infrastructure.RiskAssessment.Context
{
    public class RiskAssessmentContext : BaseDbContext
    {
        public RiskAssessmentContext()
        {
        }

        public RiskAssessmentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RiskMaster> RiskMasters { get; set; }
        public DbSet<RiskItem> RiskItems { get; set; }
        public DbSet<RiskItemContent> RiskItemContents { get; set; }
        public DbSet<RiskMaster> RiskMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RiskMaster>()
            .HasMany(rm => rm.RiskItems)
            .WithOne(ri => ri.RiskMaster)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RiskMaster>()
            .HasMany(rm => rm.RiskMappings)
            .WithOne(ri => ri.RiskMaster)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RiskItem>()
                .HasMany(ri => ri.RiskItemContents)
                .WithOne(ric => ric.RiskItem)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
