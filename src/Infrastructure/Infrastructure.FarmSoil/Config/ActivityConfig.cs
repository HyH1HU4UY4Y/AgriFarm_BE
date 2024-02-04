using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Soil.Config
{
    public class ActivityConfig : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder
                .Ignore(e => e.Participants)
                .Ignore(e => e.IsCompleted)
                .Ignore(e => e.CompletedAt)
                .Ignore(e => e.StartIn)
                .Ignore(e => e.EndIn)
                .Ignore(e => e.Addtions)
                .Ignore(e => e.Season)
                .Ignore(e => e.SeasonId)
                .Ignore(e => e.Notes)
                .Ignore(e => e.Description)
                ;
        }
    }
}
