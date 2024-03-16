using AutoMapper;
using Service.RiskAssessment.Commands;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<RiskMaster, RiskMasterDTO>().ReverseMap();
            CreateMap<RiskItem, RiskItemDTO>().ReverseMap();
            CreateMap<RiskItemContent, RiskItemContentDTO>().ReverseMap();
            CreateMap<RiskMapping, RiskMappingDTO>().ReverseMap();

            CreateMap<RiskMaster, CreateRiskMasterCommand>().ReverseMap();
            CreateMap<RiskItem, CreateRiskMasterCommand>().ReverseMap();
        }
    }
}
