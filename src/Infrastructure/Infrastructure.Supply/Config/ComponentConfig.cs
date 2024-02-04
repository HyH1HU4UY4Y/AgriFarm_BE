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
                .Ignore(e => e.Unit)
                ;

            builder.UseTpcMappingStrategy();
        }
    }
}
