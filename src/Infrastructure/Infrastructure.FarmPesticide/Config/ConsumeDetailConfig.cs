using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pesticide.Config
{
    public class ConsumeDetailConfig : IEntityTypeConfiguration<AdditionOfActivity>
    {
        public void Configure(EntityTypeBuilder<AdditionOfActivity> builder)
        {
            builder
                .Ignore(e => e.Activity)
                //.Ignore(e => e.ActivityId)
                //.Ignore(e => e.)
                ;
        }
    }
}
