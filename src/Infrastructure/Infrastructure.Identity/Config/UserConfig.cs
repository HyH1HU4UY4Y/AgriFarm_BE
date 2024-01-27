using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Config
{
    public class UserConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Users");

            builder
                .HasMany(e => e.Certificates)
                .WithOne(e => e.Member)
                .HasForeignKey(e => e.MemberId);

            builder
                .Ignore(e=>e.Activities)
                ;

            
        }
    }
}
