using AutoMapper;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<RiskMaster, RiskMasterDTO>().ReverseMap();
            CreateMap<RiskMaster, RiskItem>().ReverseMap();
        }
    }
}
