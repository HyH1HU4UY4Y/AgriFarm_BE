using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

namespace Service.Payment.DTOs.MerchantDTOs
{
    public class MerchantRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
    }

    public class MerchantInsertRequest
    {
        public string? MerchantName { get; set; }
        public string? MerchantWebLink { get; set; }
        public string? MerchantIpnUrl { get; set; }
        public string? MerchantReturnUrl { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class MerchantSetActiveRequest
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class MerchantUpdateRequest
    {
        public Guid Id { get; set; }
        public string? MerchantName { get; set; }
        public string? MerchantWebLink { get; set; }
        public string? MerchantIpnUrl { get; set; }
        public string? MerchantReturnUrl { get; set; }
        public string? SecretKey { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
