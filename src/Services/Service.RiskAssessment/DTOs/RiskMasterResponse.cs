﻿namespace Service.RiskAssessment.DTOs
{
    public class Pagination
    {
        public int perPage { get; set; } = 10;
        public int pageId { get; set; } = 1;
        public int totalRecord { get; set; } = 0;
    }
    public class ResponseStatus
    {
        public int statusCode { get; set; }
        public List<string>? message { get; set; }
    }
    public class RiskMasterResponse : ResponseStatus
    {
        public List<RiskMasterDTO> data { get; set; } = new List<RiskMasterDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class RiskDetailResponse : ResponseStatus
    {
        public RiskMasterDTO? data { get; set; }
    }

    public class RiskAssessmentInsertResponse : ResponseStatus
    {
    }

    public class RiskAssessmentUpdateResponse : ResponseStatus
    {
    }

    public class RiskAssessmentDeleteResponse : ResponseStatus
    {
    }

    public class RiskAssessmentImplResponse : ResponseStatus { }
    public class RiskAssessmentMappingResponse : ResponseStatus { }
    public class RiskAssessmentListMappingResponse : ResponseStatus {
        public List<RiskMappingDTO> data { get; set; } = new List<RiskMappingDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class RiskAssessmentCheckStatus : ResponseStatus
    {
        public int status { get; set; }
    }
}
