using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class PaymentDestination : BaseEntity
    {
        public string DesName { get; set; } = string.Empty;
        public string DesShortName { get; set; } = string.Empty;
        public string DesParentId { get; set; } = string.Empty;
        public string DesLogo { get; set; } = string.Empty;
        public int SortIndex { get; set; }
        public bool IsActive { get; set; }
    }
}
