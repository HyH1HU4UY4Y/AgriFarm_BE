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
        public string? RiskName { get; set; }
        public string? RiskDescription { get; set; }
        public Guid? CreateBy { get; set; }
        public List<RiskItem>? RiskItems { get; set; }
    }
}
