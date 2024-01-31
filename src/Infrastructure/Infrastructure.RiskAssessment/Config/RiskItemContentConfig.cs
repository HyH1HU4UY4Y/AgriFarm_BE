using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Diagnosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Risk;

namespace Infrastructure.RiskAssessment.Config
{
    public class RiskItemContentConfig : IEntityTypeConfiguration<RiskItemContent>
    {
        public void Configure(EntityTypeBuilder<RiskItemContent> builder)
        {
            builder.Property(e=>e.Id).HasColumnName("RiskItemContentId");
        }
    }
}
