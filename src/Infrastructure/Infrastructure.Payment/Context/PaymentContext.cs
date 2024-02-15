using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.Pay;

namespace Infrastructure.Payment.Context
{
    public class PaymentContext : BaseDbContext
    {
        public PaymentContext()
        {
        }

        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<SharedDomain.Entities.Pay.Paymentt> Paymentts { get; set; }
        public DbSet<PaymentDestination> PaymentDestinations { get; set; }
        public DbSet<PaymentNotification> PaymentNotifications { get; set; }
        public DbSet<PaymentSignature> PaymentSignatures { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
