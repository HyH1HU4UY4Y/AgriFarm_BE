using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Schedules;

namespace Infrastructure.FarmScheduling.Config
{
    public class SeasonConfig : IEntityTypeConfiguration<CultivationSeason>
    {
        public void Configure(EntityTypeBuilder<CultivationSeason> builder)
        {
            builder
                .Ignore(e => e.Products);
        }
    }
}
