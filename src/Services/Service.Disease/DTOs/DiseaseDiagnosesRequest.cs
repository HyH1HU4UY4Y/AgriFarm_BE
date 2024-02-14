using SharedDomain.Defaults;

namespace Service.Disease.DTOs
{
    public class DiseaseDiagnosesRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set;}

    }
    public class DiseaseDiagnosesUpdateRequest
    {
        public Guid Id { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; }
    }
    public class DiseaseDiagnosesInsertRequest
    {
        public Guid PlantDiseaseId { get; set; }
        public string? Description { get; set; }
        public string? Feedback { get; set; }
        public string? Location { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? LandId { get; set; }
    }

    public class FeedbackUpdateRequest
    {
        public Guid Id { get; set; }
        public string? Feedback { get; set; }
    }
}
