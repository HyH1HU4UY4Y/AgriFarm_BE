using AutoMapper;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules;
using Service.Training.DTOs;
using SharedDomain.Entities.Schedules.Training;

namespace Service.Training.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TrainingDetailRequest, TrainingDetail>().ReverseMap();
            CreateMap<TrainingDetail, TrainingDetailResponse>().ReverseMap();
            
            CreateMap<ExpertRequest, ExpertInfo>().ReverseMap();
            CreateMap<ExpertInfo, ExpertResponse>().ReverseMap();

        }
    }
}
