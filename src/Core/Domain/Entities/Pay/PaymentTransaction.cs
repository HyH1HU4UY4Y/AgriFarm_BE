using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class PaymentTransaction : BaseEntity
    {
        public string? TranMessage { get; set; } = string.Empty;
        public string? TranPayload { get; set; } = string.Empty;
        public string? TranStatus { get; set; } = string.Empty;
        public decimal? TranAmount { get; set; }
        public DateTime? TranDate { get; set; }
        public Guid? PaymentId { get; set; }
        public Paymentt? Payment { get; set; }
        public Guid? TranRefId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
