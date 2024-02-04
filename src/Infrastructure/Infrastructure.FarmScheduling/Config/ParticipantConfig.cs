using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmScheduling.Config
{
    public class ParticipantConfig : IEntityTypeConfiguration<MinimalUserInfo>
    {
        public void Configure(EntityTypeBuilder<MinimalUserInfo> builder)
        {
            
        }
    }
}
