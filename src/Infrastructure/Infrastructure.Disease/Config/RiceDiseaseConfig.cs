using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Diagnosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Disease.Config
{
    public class RiceDiseaseConfig : IEntityTypeConfiguration<DiseaseInfo>
    {
        public void Configure(EntityTypeBuilder<DiseaseInfo> builder)
        {
            //builder.Property(e=>e.Id).HasColumnName("plant_disease_id");
        }
    }
}
