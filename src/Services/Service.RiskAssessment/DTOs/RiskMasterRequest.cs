namespace Service.RiskAssessment.DTOs
{
    public class RiskMasterRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
    }
}
