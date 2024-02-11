namespace Service.Payment.DTOs.PaymentDTOs
{

    public class PaymentRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
    }
    public class PaymentInsertRequest
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public Guid PaymentRefId { get; set; }
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        public string? PaymentLanguage { get; set; } = string.Empty;
        public Guid? MerchantId { get; set; }
        public Guid? PaymentDestinationId { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? Signature { get; set; } = string.Empty;
    }
}
