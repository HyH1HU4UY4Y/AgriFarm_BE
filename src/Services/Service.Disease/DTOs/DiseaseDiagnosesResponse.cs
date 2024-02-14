namespace Service.Disease.DTOs
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
    public class DiseaseDiagnosesResponse : ResponseStatus
    {
        public List<DiseaseDiagnosesDTO> data { get; set; } = new List<DiseaseDiagnosesDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class DiseaseDiagnosesDetailResponse : ResponseStatus
    {
        public DiseaseDiagnosesDTO? data { get; set; }
    }
    public class DiseaseDiagnosesUpdateResponse : ResponseStatus
    {
    }
    public class DiseaseDiagnosesInsertResponse : ResponseStatus 
    {
        public DiseaseDiagnosesDTO? data { get; set; }
    }
    public class DiseaseDiagnosesExportResponse : ResponseStatus
    {
        public string? data { get; set; }
    }

}
