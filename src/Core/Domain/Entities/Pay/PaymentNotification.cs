using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class PaymentNotification : BaseEntity
    {
        public string? PaymentRefId { get; set; } = string.Empty;
        public DateTime? Date {  get; set; }
        public string? NotiContent { get; set; } = string.Empty;
        public decimal NotiAmount { get; set; }
        public string? NotiSignature { get; set; } = string.Empty;
        public string? NotiPaymentId { get; set; } = string.Empty;
        public Paymentt Paymentt { get; set; }
        public string? NotiMerchantId {  get; set; } = string.Empty;
        public string? NotiStatus {  get; set; } = string.Empty;
        public DateTime? NotiResDate { get; set; }
        public string? NotiResMessage { get; set; } = string.Empty;
        public string? NotiResHttpCode { get; set; } = string.Empty;

    }
}
