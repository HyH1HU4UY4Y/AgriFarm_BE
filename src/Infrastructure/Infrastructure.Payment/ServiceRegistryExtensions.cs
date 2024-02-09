using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Persistence;
using SharedApplication.MultiTenant;
using SharedDomain.Entities.Pay;
using Infrastructure.Payment.Context;

namespace Infrastructure.Payment
{
    
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultSQLDB<PaymentContext>(configuration)
                    .AddMultiTenant(configuration);

            services
                .AddSQLRepo<PaymentContext, Merchant > ()
                .AddSQLRepo<PaymentContext, Paymentt > ()
                .AddSQLRepo<PaymentContext, PaymentDestination > ()
                .AddSQLRepo<PaymentContext, PaymentNotification > ()
                .AddSQLRepo<PaymentContext, PaymentSignature > ()
                .AddSQLRepo<PaymentContext, PaymentTransaction > ();


            return services;
        }
    }
}
