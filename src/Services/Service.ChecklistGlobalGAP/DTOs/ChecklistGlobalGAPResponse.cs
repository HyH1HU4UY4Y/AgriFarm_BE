using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Service.ChecklistGlobalGAP.DTOs
{
    public class ResponseStatus
    {
        public int statusCode { get; set; }
        public List<string>? message { get; set; }
    }
    public class ChecklistGlobalGAPGetListResponse : ResponseStatus
    {
        public List<ChecklistMapping> data { get; set; } = new List<ChecklistMapping>();
    }
}
