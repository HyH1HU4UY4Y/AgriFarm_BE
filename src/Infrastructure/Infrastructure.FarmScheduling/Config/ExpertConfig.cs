using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmScheduling.Config
{
    public class ExpertConfig : IEntityTypeConfiguration<ExpertInfo>
    {
        
        public void Configure(EntityTypeBuilder<ExpertInfo> builder)
        {
            builder
                .Ignore(e => e.Certificates)
                .Ignore(e => e.ExpertField)
                ;
        }
    }
}
