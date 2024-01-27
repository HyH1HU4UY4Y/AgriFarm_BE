using SharedDomain.Defaults;

namespace Service.Identity.DTOs
{
    public class CertificateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public string? Resource { get; set; }
    }
}
