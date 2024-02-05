using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder
                .Ignore(e => e.Properties)
                .Ignore(e => e.Notes)
                .Ignore(e => e.Description)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                .Ignore(e => e.Documents)
                .Ignore(e => e.Resource)
                .Ignore(e => e.Unit)
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

    public class ComponentSoilConfig : IEntityTypeConfiguration<FarmSoil>
    {
        public void Configure(EntityTypeBuilder<FarmSoil> builder)
        {
            builder
                .Ignore(e => e.Acreage)
                .Ignore(e => e.Position)
                ;

            builder.UseTphMappingStrategy();
        }
    }
    public class ComponentWaterConfig : IEntityTypeConfiguration<FarmWater>
    {
        public void Configure(EntityTypeBuilder<FarmWater> builder)
        {
            builder
                .Ignore(e => e.Properties)
                .Ignore(e => e.Notes)
                .Ignore(e => e.Description)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                .Ignore(e => e.Documents)
                .Ignore(e => e.Unit)
                ;

            builder.UseTphMappingStrategy();
        }
    }
}
