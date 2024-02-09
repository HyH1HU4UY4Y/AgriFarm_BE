using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Pay;

namespace Infrastructure.Payment.Config
{
    public class PaymentConfig : IEntityTypeConfiguration<Paymentt>
    {
        public void Configure(EntityTypeBuilder<Paymentt> builder)
        {
            builder
                .Ignore(e => e.IsDeleted)
                .Ignore(e => e.DeletedDate)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify);
        }
    }
}
