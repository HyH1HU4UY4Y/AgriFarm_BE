using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedApplication.Persistence.Configs;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Supply.Config
{
    public class ComponentConfig : IEntityTypeConfiguration<BaseComponent>
    {
        public void Configure(EntityTypeBuilder<BaseComponent> builder)
        {
            builder.ExtractComponent()
                    .Ignore(e => e.IsConsumable);

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

    public class ComponentSoilConfig : IEntityTypeConfiguration<FarmSoil>
    {
        public void Configure(EntityTypeBuilder<FarmSoil> builder)
        {
            builder.ExtractSoil();

            builder.UseTphMappingStrategy();
        }
    }
    public class ComponentWaterConfig : IEntityTypeConfiguration<FarmWater>
    {
        public void Configure(EntityTypeBuilder<FarmWater> builder)
        {
            /*builder.Ex
                ;*/

            builder.UseTphMappingStrategy();
        }
    }
}
