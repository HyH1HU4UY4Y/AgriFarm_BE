using AutoMapper;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules;
using Service.Training.DTOs;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Training;

namespace Service.Training.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DetailRequest, TrainingDetail>().ReverseMap();
            CreateMap<TrainingDetail, DetailResponse>().ReverseMap();
            
            CreateMap<ExpertRequest, ExpertInfo>().ReverseMap();
            CreateMap<ExpertInfo, ExpertResponse>().ReverseMap();
            CreateMap<ExpertInfo, FullExpertResponse>().ReverseMap();

            CreateMap<ContentRequest, TrainingContent>().ReverseMap();
            CreateMap<TrainingContent, ContentResponse>().ReverseMap();
            CreateMap<TrainingContent, FullContentResponse>().ReverseMap();

        }
    }
}
