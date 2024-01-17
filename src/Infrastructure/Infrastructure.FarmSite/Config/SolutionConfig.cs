using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmSite.Config
{
    public class SolutionConfig : IEntityTypeConfiguration<PackageSolution>
    {
        public void Configure(EntityTypeBuilder<PackageSolution> builder)
        {
            builder.ToTable("SolutionDetails");

            builder
                .Ignore(e => e.Description)
                .Ignore(e => e.DurationInMonth)
                .Ignore(e => e.Subscripts)
                .Ignore(e => e.Price)
                ;
        }
    }
}
