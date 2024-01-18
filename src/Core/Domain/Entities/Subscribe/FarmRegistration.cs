using SharedDomain.Defaults;
using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain.Entities.Subscribe
{
    public class FarmRegistration : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Phone]
        public string? Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(5000)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string SiteCode { get; set; }
        public string SiteName { get; set; }


        public Guid SolutionId { get; set; }
        public PackageSolution? Solution { get; set; }

        public decimal Cost { get; set; }
        public string? PaymentDetail { get; set; }

        public DecisonOption? IsApprove { get; set; } = DecisonOption.Waiting;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModify { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
