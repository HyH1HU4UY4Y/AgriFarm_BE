using System.ComponentModel.DataAnnotations;

namespace Service.Training.DTOs
{
    public class ExpertRequest
    {
        [Required]
        
        public string FullName { get; set; }
        public string Description { get; set; }
        //[Required]
        public string ExpertField { get; set; }
        
    }
}
