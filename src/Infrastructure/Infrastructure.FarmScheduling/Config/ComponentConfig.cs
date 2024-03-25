using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedApplication.Persistence.Configs;

namespace Infrastructure.FarmScheduling.Config
{
    public class ComponentConfig : IEntityTypeConfiguration<BaseComponent>
    {
        public void Configure(EntityTypeBuilder<BaseComponent> builder)
        {

            builder.ExtractComponent()
                ;
            builder.UseTphMappingStrategy()
                .HasDiscriminator<string>("Type")
                .HasValue<FarmSoil>(nameof(FarmSoil).Substring(4))
                .HasValue<FarmSeed>(nameof(FarmSeed).Substring(4))
                .HasValue<FarmFertilize>(nameof(FarmFertilize).Substring(4))
                .HasValue<FarmPesticide>(nameof(FarmPesticide).Substring(4))
                .HasValue<FarmWater>(nameof(FarmWater).Substring(4))
                .HasValue<FarmEquipment>(nameof(FarmEquipment).Substring(4));
        }
    }
}
