using Service.Payment.DTOs.MerchantDTOs;

namespace Service.Payment.DTOs.PaymentDTOs
{

    public class ResponseStatus
    {
        public int statusCode { get; set; }
        public List<string>? message { get; set; }
    }

    public class MPaymentResponse : ResponseStatus
    {
        public List<PaymentDTO> data { get; set; } = new List<PaymentDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class PaymentInsertResponse : ResponseStatus
    {
        public Guid? Id { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
    }
}
