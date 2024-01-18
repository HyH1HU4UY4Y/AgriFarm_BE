using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

namespace Service.Registration.DTOs
{
    public class ResolveFormRequest
    {
        public DecisonOption Decison { get; set; }
        [StringLength(3000)]
        public string Notes { get; set; }
    }
}
