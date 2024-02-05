using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class CertificateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
