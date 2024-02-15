
using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class Paymentt : BaseEntity
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public Guid PaymentRefId { get; set; }
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; }
        public string? PaymentLanguage { get; set; } = string.Empty;
        public Guid? MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public Guid? PaymentDestinationId { get; set; } 
        public PaymentDestination? PaymentDestination { get; set; }
        public decimal? PaidAmount { get; set; }
        public string? PaymentStatus { get; set; } = string.Empty;
        public string? PaymentLastMessage { get; set; } = string.Empty;

        public Guid? CreatedBy { get; set; }
    }
}
