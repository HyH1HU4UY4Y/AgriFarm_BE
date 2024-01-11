using SharedDomain.Defaults;
using SharedDomain.Entities.Subscribe;

namespace Service.FarmRegistry.DTOs
{
    public class RegisterFormResponse
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        public string SiteKey { get; set; }
        public string SiteName { get; set; }

        public DecisonOption? IsApprove { get; set; }
        public SolutionResponse Solution { get; set; }
        public decimal Cost { get; set; }
        public string PaymentDetail { get; set; }
    }
}
