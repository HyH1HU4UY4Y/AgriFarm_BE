using SharedDomain.Entities.Risk;
using System.ComponentModel.DataAnnotations;

namespace Service.RiskAssessment.DTOs
{
    public class RiskMasterRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool? isDraft { get; set; }
    }

    public class RiskAssessmentInsertRequest
    {
        public string? riskName { get; set; }
        public string? riskDescription { get; set; }
        public Guid? createBy { get; set; }
        public bool isDraft { get; set; } = true;
        public List<RiskAssessmentItemDef>? riskItems { get; set; }
    }
    public class RiskAssessmentItemDef
    {
        public string? riskItemTile { get; set; }
        public int? riskItemDiv { get; set; }
        public int? riskItemType { get; set; }
        public string? riskItemContent { get; set; }
        public int? must { get; set; }

    }
    public class RiskAssessmentItemUpdDef : RiskAssessmentItemDef
    {
        public Guid itemId { get; set; }
    }
    public class RiskAssessmentUpdateRequest
    {
        public string? riskName { get; set; }
        public string? riskDescription { get; set; }
        public Guid? createBy { get; set; }
        public List<RiskAssessmentItemUpdDef>? riskItems { get; set; }
    }
    public class RiskAssessmentImplDef
    {
        public Guid riskItemId { get; set; }
        public Guid riskMappingId { get; set; }
        public string? answer { get; set; }
    }
    public class RiskAssessmentImplRequset
    {
        public List<RiskAssessmentImplDef>? riskAssessmentImpl { get; set; }
    }
    public class RiskAssessmentMappingRequest
    {
        public Guid riskMasterId { get; set; }
        public Guid taskId { get; set; }
    }
    public class RiskAssessmentListMappingRequest
    {
        public Guid taskId { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }

    }
    public class RiskAssessmentCheckStatusRequest
    {
        public Guid riskMasterId { get; set; }
        public Guid riskMappingId { get; set; }
    }
}
