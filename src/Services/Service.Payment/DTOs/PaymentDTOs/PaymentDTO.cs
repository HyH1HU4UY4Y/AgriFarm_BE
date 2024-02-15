using SharedDomain.Entities.Pay;

namespace Service.Payment.DTOs
{
    public class PaymentDTO : Paymentt
    {
        /*public string Id { get; set; } = string.Empty;
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; }
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchantId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;
        public string? PaymentStatus { get; set; } = string.Empty;
        public decimal? PaidAmount { get; set; }*/
        public string PaymentUrl { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;

    }
}
