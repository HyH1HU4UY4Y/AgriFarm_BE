using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Schedules;
using SharedApplication.Persistence.Configs;

namespace Infrastructure.FarmScheduling.Config
{
    public class SeasonConfig : IEntityTypeConfiguration<CultivationSeason>
    {
        public void Configure(EntityTypeBuilder<CultivationSeason> builder)
        {
            builder.ExtractSeason()
                ;
            //builder.ToTable("Locations");


        }
    }
}
