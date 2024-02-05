using AutoMapper;
using Service.FarmScheduling.DTOs;
using SharedDomain.Entities.Schedules;

namespace Service.FarmScheduling.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            
            
            CreateMap<ActivityRequest, Activity>().ReverseMap();
            CreateMap<Activity, ActivityResponse>().ReverseMap();

            CreateMap<Tag, TagResponse>().ReverseMap();

        }
    }
}
