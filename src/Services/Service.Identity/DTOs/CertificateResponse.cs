using Newtonsoft.Json;

namespace Service.Identity.DTOs
{
    public class CertificateResponse
    {
        public Guid Id { get; set; }
        //[JsonProperty("certificate_name")]
        public string Name { get; set; }
    }
}
