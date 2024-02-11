using AutoMapper;
using Service.Payment.Commands;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs;
using Service.Payment.DTOs.MerchantDTOs;
using Service.Payment.DTOs.PaymentDTOs;
using SharedDomain.Entities.Pay;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Merchant, MerchantDTO>().ReverseMap();
            CreateMap<Merchant, CreateMerchantCommand>().ReverseMap();

            CreateMap<Paymentt, PaymentLinkDTO>().ReverseMap();
            CreateMap<Paymentt, CreatePaymentCommand>().ReverseMap();
            CreateMap<Paymentt, PaymentDTO>().ReverseMap();
            CreateMap<Paymentt, PaymentInsertRequest>().ReverseMap();
            CreateMap<Paymentt, PaymentInsertResponse>().ReverseMap();
            /*CreateMap<DiseaseInfo, UpdateDiseaseInfoCommand>().ReverseMap();
            CreateMap<DiseaseInfo, DeleteDiseaseInfoCommand>().ReverseMap();

            CreateMap<DiseaseDiagnosis, DiseaseDiagnosesDTO>().ReverseMap();*/
        }
    }
}
