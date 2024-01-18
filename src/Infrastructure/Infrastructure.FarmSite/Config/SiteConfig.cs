using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmSite.Config
{
    public class SiteConfig : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder
                .Ignore(e => e.Components);
/*
            builder.HasData(new List<Site>{
                new()
                {
                    SiteCode = "xyz.abc.vn",
                    Name = "Farm 01",
                    IsActive = false,
                    Intro = "A very  long   intro"
                },
                new()
                {
                    SiteCode = "asd.fgh.vn",
                    Name = "Farm 02",
                    IsActive = false,
                    Intro = "A very  long   intro"
                }
            });*/
        }
    }
}
