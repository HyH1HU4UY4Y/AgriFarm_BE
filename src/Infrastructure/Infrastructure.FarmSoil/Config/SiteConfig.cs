using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Soil.Config
{
    public class SiteConfig : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder
                .Ignore(e => e.PaymentDetail)
                .Ignore(e => e.Intro)
                .Ignore(e => e.Capitals)
                .Ignore(e => e.Subscripts)
                .Ignore(e => e.AvatarImg)
                .Ignore(e => e.LogoImg)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                ;


        }
    }
}
