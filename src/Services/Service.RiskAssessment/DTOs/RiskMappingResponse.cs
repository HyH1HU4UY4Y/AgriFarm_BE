namespace Service.RiskAssessment.DTOs
{
    public class RiskMappingResponse
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public RiskMasterMinResponse CheckList { get; set; }
    }

    public class RiskMasterMinResponse
    {
        public Guid Id { get; set; }
        public string? RiskName { get; set; }
        public string? RiskDescription { get; set; }
    }
}
