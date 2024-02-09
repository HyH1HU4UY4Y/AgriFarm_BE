using AutoMapper;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Entities.Pay;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Merchant, MerchantDTO>().ReverseMap();

            CreateMap<Merchant, CreateMerchantCommand>().ReverseMap();
            /*CreateMap<DiseaseInfo, UpdateDiseaseInfoCommand>().ReverseMap();
            CreateMap<DiseaseInfo, DeleteDiseaseInfoCommand>().ReverseMap();

            CreateMap<DiseaseDiagnosis, DiseaseDiagnosesDTO>().ReverseMap();*/
        }
    }
}
