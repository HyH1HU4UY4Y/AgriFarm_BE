using AutoMapper;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Diagnosis;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<DiseaseInfo, RiskMasterDTO>().ReverseMap();
        }
    }
}
