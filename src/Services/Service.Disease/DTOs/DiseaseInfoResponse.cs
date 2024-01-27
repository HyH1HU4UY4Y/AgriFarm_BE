namespace Service.Disease.DTOs
{

    public class DiseaseInfoResponse : ResponseStatus
    {
        public List<DiseaseInfoDTO> data { get; set; } = new List<DiseaseInfoDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class DiseaseInfoDetailResponse : ResponseStatus
    {
        public DiseaseInfoDTO? data { get; set; }
    }
    public class DiseaseInfoUpdateResponse : ResponseStatus
    {
    }
    public class DiseaseInfoInsertResponse : ResponseStatus
    {
    }

    public class DiseaseInfoDeleteResponse : ResponseStatus
    {
    }
}
