using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class PaymentSignature : BaseEntity
    {
        public Guid? PaymentId { get; set; }
        public Paymentt? Paymentt { get; set; }
        public string? SignValue { get; set; } = string.Empty;
        public string? SignAlgo { get; set; } = string.Empty;
        public string? SignOwn { get; set; } = string.Empty;
        public DateTime? SignDate { get; set; }
        public bool IsValid { get; set; }
    }
}
