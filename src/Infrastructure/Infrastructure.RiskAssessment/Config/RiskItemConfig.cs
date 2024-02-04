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
    public class RiskItemConfig : IEntityTypeConfiguration<RiskItem>
    {
        public void Configure(EntityTypeBuilder<RiskItem> builder)
        {
            builder.Property(e=>e.Id).HasColumnName("RiskItemId");
        }
    }
}
