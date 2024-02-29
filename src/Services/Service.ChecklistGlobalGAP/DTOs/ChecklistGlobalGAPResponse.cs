using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Service.ChecklistGlobalGAP.DTOs
{
    public class ResponseStatus
    {
        public int statusCode { get; set; }
        public List<string>? message { get; set; }
    }

    public class Pagination
    {
        public int perPage { get; set; } = 10;
        public int pageId { get; set; } = 1;
        public int totalRecord { get; set; } = 0;
    }

    public class ChecklistGlobalGAPGetListResponse : ResponseStatus
    {
        public List<ChecklistMappingDTO> data { get; set; } = new List<ChecklistMappingDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class ChecklistGlobalGAPAddListResponse : ResponseStatus
    {
        public ChecklistMapping? data { get; set; }
    }
    public class ChecklistGlobalGAPCreateResponse : ResponseStatus
    {
        public ChecklistMaster? data { get; set; }
    }
    public class ChecklistGlobalGAPAddItemResponse : ResponseStatus { }
    public class ChecklistGlobalGAPDeleteListResponse : ResponseStatus { }
    public class ChecklistGlobalGAPGetChecklistResponse : ResponseStatus
    {
        public ChecklistMappingDTO? data { get; set; }
    }
}
