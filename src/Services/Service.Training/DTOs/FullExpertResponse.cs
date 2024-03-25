using SharedDomain.Entities.Training;

namespace Service.Training.DTOs
{
    public class FullExpertResponse
    {
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public string? ExpertField { get; set; }
        public List<ExpertCertification> Certificates { get; set; }
    }
}
