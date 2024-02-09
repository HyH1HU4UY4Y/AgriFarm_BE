using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.Pay;

namespace Infrastructure.Payment.Config
{
    public class PaymentNotificationConfig : IEntityTypeConfiguration<PaymentNotification>
    {
        public void Configure(EntityTypeBuilder<PaymentNotification> builder)
        {
            builder
                .Ignore(e => e.IsDeleted)
                .Ignore(e => e.DeletedDate)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                ;
        }
    }
}
