using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Pay
{
    public class Merchant : BaseEntity
    {
        public string? MerchantName {  get; set; } = string.Empty;
        public string? MerchantWebLink {  get; set; } = string.Empty;
        public string? MerchantIpnUrl {  get; set; } = string.Empty;
        public string? MerchantReturnUrl {  get; set; } = string.Empty;
        public string SecretKey {  get; set; } = string.Empty;
        public bool IsActive {  get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastUpdatedBy { get; set; }

    }
}
