using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.Diagnosis;

namespace Infrastructure.Disease.Context
{
    public class DiseaseContext : BaseDbContext
    {
        public DiseaseContext()
        {
        }

        public DiseaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DiseaseInfo> DiseaseInfos { get; set; }
        public DbSet<DiseaseDiagnosis> DiseaseDiagnoses { get; set; }

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
