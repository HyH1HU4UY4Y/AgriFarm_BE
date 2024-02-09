namespace Service.Payment.DTOs.PaymentSignature
{
    public class PaymentSignatureDTO
    {
        public string? PaymentId { get; set; } = string.Empty;
        public string? SignValue { get; set; } = string.Empty;
        public string? SignAlgo { get; set; } = string.Empty;
        public string? SignOwn { get; set; } = string.Empty;
        public DateTime? SignDate { get; set; }
        public bool IsValid { get; set; }
    }
}
