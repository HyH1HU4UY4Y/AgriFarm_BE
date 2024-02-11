namespace Service.Payment.DTOs
{
    public class PaymentLinkDTO
    {
        public Guid? PaymentId { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
    }
}
