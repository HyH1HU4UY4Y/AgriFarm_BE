using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmCultivation.Config
{
    public class ParticipantConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Participant");

            builder
                .Ignore(e => e.DOB)
                .Ignore(e => e.AccessFailedCount)
                .Ignore(e => e.Address)
                .Ignore(e => e.ConcurrencyStamp)
                .Ignore(e => e.Email)
                .Ignore(e => e.EmailConfirmed)
                .Ignore(e => e.Education)
                .Ignore(e => e.Gender)
                .Ignore(e => e.IdentificationCard)
                .Ignore(e => e.NormalizedEmail)
                .Ignore(e => e.NormalizedUserName)
                .Ignore(e => e.PasswordHash)
                .Ignore(e => e.PhoneNumber)
                ;
        }
    }
}
