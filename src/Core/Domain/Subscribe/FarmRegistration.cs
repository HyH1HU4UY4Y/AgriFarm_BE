using SharedDomain.Base;
using SharedDomain.Defaults;

namespace SharedDomain.Subscribe
{
    public class FarmRegistration : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        public string SiteKey { get; set; }
        public string SiteName { get; set; }


        public Guid SolutionId { get; set; }
        public PackageSolution Solution { get; set; }

        public decimal Cost { get; set; }
        public string PaymentDetail { get; set; }

        public DecisonOption IsApprove { get; set; } = DecisonOption.Waiting;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
