using SharedDomain.Defaults;
using System.ComponentModel.DataAnnotations;

namespace Service.Disease.DTOs
{
    public class DiseaseInfoRequest
    {
        public string? keyword { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
    }

    public class DiseaseInfoInsertRequest
    {
        public string? DiseaseName { get; set; }
        [StringLength(8000)]
        public string? Symptoms { get; set; }
        [StringLength(8000)]
        public string? Cause { get; set; }
        public string? PreventiveMeasures { get; set; }
        public string? Suggest { get; set; }
        public Guid? CreateBy { get; set; }
    }

    public class DiseaseInfoUpdateRequest
    {
        public Guid Id { get; set; }
        public string? DiseaseName { get; set; }
        [StringLength(8000)]
        public string? Symptoms { get; set; }
        [StringLength(8000)]
        public string? Cause { get; set; }
        public string? PreventiveMeasures { get; set; }
        public string? Suggest { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
    }
}
