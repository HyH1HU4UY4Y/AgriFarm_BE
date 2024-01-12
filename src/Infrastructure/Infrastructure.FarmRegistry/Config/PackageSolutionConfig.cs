using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Registration.Config
{
    public class PackageSolutionConfig : IEntityTypeConfiguration<PackageSolution>
    {
        public void Configure(EntityTypeBuilder<PackageSolution> builder)
        {
            builder.Ignore(x => x.Subscripts);
            

            builder.HasData(new List<PackageSolution>()
            {
                new()
                {
                    Description = "This is cheapest solution",
                    Name = "Solution 1",
                    Price = 10,
                    DurationHour = 750,

                },
                new()
                {
                    Description = "This is medium solution",
                    Name = "Solution 2",
                    Price = 100,
                    DurationHour = 7800,

                },
                new()
                {
                    Description = "This is vip solution",
                    Name = "Solution 3",
                    Price = 1000,
                    DurationHour = 79000,

                }
            });
        }
    }
    
}
