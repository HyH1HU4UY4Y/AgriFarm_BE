using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedApplication.Persistence.Configs;
using SharedDomain.Entities.Schedules;

namespace Infrastructure.Soil.Config
{
    public class ActivityConfig : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ExtractActivity();
        }
    }
}
