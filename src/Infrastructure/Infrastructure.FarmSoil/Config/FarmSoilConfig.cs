using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Soil.Config
{
    public class FarmSoilConfig : IEntityTypeConfiguration<FarmSoil>
    {
        public void Configure(EntityTypeBuilder<FarmSoil> builder)
        {

            builder.ToTable("Lands");
            builder
                .HasMany(x => x.States)
                .WithOne(x => x.Component as FarmSoil)
                .HasForeignKey(x => x.ComponentId);
            
            builder
                .HasMany(x => x.Properties)
                .WithOne(x => x.Component as FarmSoil)
                .HasForeignKey(x => x.ComponentId);


        }
    }
}
