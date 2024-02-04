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
            builder.HasData(
                new FarmRegistration[]
                {
                    new()
                    {
                        Email = "owner01@test.com",
                        SolutionId = new Guid("3c9cca4d-0899-45de-951e-8a3e8364758c"),
                        Phone = "0132302225",
                        FirstName = "User",
                        LastName = "Owner 01",
                        Address = "USA",
                        Cost = new Decimal(10),
                        SiteCode = "test.agri.01",
                        PaymentDetail = "test detail",
                        SiteName = "Farm 01 test"
                    }
                }
            ) ;
        }
    }
}
