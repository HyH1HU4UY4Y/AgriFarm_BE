using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
