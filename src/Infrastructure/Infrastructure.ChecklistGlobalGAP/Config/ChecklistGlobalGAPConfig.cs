using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.ChecklistGlobalGAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ChecklistGlobalGAP.Config
{
    public class ChecklistGlobalGAPConfig : IEntityTypeConfiguration<ChecklistMaster>
    {
        public void Configure(EntityTypeBuilder<ChecklistMaster> builder)
        {
            //builder.Property(e=>e.Id).HasColumnName("plant_disease_id");
        }
    }
}
