using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmCultivation.Config
{
    public class ComponentConfig : IEntityTypeConfiguration<BaseComponent>
    {
        public void Configure(EntityTypeBuilder<BaseComponent> builder)
        {
            builder.ToTable("FarmComponents");

            builder
                .Ignore(e => e.Notes)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                .Ignore(e => e.States)
                .Ignore(e => e.Properties)
                .Ignore(e => e.Documents)
                ;

            



            
        }
    }
}
