using SharedDomain.Entities.Subscribe;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Events
{
    public class AcceptFarmRegistEvent
    {
        public string FarmName { get; set; }
        public string FarmKey { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string? Phone { get; set; }

        public string Email { get; set; }

        public string? Address { get; set; }


        public string SiteCode { get; set; }
        public string SiteName { get; set; }


        public Guid SolutionId { get; set; }

        public decimal Cost { get; set; }
        public string? PaymentDetail { get; set; }
        public long? DurationInMonth { get; set; }

    }
}
