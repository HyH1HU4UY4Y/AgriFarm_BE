using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Water.Config
{
    public class WaterConfig : IEntityTypeConfiguration<FarmWater>
    {
        public void Configure(EntityTypeBuilder<FarmWater> builder)
        {

            builder.ToTable("WaterSources");
            builder
                .HasMany(x => x.States)
                .WithOne(x => x.Component as FarmWater)
                .HasForeignKey(x => x.ComponentId);

            builder
                .HasMany(x => x.Properties)
                .WithOne(x => x.Component as FarmWater)
                .HasForeignKey(x => x.ComponentId);


        }
    }
}
