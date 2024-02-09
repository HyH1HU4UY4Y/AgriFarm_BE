namespace Service.Payment.DTOs.MerchantDTOs
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
    public class MerchantResponse : ResponseStatus
    {
        public List<MerchantDTO> data { get; set; } = new List<MerchantDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class MerchantDetailResponse : ResponseStatus
    {
        public MerchantDTO? data { get; set; }
    }

    public class MerchantInsertResponse : ResponseStatus
    {
    }

    public class MerchantSetActiveResponse : ResponseStatus
    {
    }

    public class MerchantUpdateResponse : ResponseStatus
    {
    }

    

    public class MerchantDeleteResponse : ResponseStatus
    {
    }
}
