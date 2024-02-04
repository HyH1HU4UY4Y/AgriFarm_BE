using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Registration.Config
{
    internal class FarmRegistrationConfig: IEntityTypeConfiguration<FarmRegistration>
    {
        public void Configure(EntityTypeBuilder<FarmRegistration> builder)
        {
            
        }
    }
}
