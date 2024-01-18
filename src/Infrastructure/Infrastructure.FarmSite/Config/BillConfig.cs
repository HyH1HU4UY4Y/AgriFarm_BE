using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmSite.Config
{
    public class BillConfig : IEntityTypeConfiguration<Subscripton>
    {
        public void Configure(EntityTypeBuilder<Subscripton> builder)
        {
            builder
                .Ignore(e => e.Solution);
        }
    }
}
