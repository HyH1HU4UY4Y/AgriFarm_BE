namespace Service.RiskAssessment.DTOs
{
    public class RiskMasterRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }

        public bool? isDraft { get; set; }
    }
}
