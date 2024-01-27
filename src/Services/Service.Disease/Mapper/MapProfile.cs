using AutoMapper;
using Service.Disease.Commands;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;

namespace Service.Disease.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<DiseaseInfo, DiseaseInfoDTO>().ReverseMap();

            CreateMap<DiseaseInfo, DiseaseInfoDTO>().ReverseMap();
            CreateMap<DiseaseInfo, CreateDiseaseInfoCommand>().ReverseMap();
            CreateMap<DiseaseInfo, UpdateDiseaseInfoCommand>().ReverseMap();
            CreateMap<DiseaseInfo, DeleteDiseaseInfoCommand>().ReverseMap();

            CreateMap<DiseaseDiagnosis, DiseaseDiagnosesDTO>().ReverseMap();
        }
    }
}
