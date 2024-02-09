using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Pay;

namespace Infrastructure.Payment.Config
{
    public class PaymentDestinatonConfig : IEntityTypeConfiguration<PaymentDestination>
    {
        public void Configure(EntityTypeBuilder<PaymentDestination> builder)
        {
            builder
                .Ignore(e => e.IsDeleted)
                .Ignore(e => e.DeletedDate)
                ;
        }
    }
}
