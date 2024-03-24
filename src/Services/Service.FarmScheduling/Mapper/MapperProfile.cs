using AutoMapper;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.DTOs.Details;
using SharedDomain.Defaults.Converters;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Users;

namespace Service.FarmScheduling.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            
            
            CreateMap<ActivityCreateRequest, Activity>().ReverseMap();
            CreateMap<Activity, ActivityResponse>().ReverseMap();

            CreateMap<AdditionOfActivity, AdditionResponse>().ReverseMap();

            CreateMap<Tag, TagResponse>().ReverseMap();

            CreateMap<CultivationSeason, SeasonResponse>().ReverseMap();
            CreateMap<FarmSoil, LandResponse>().ReverseMap();

            CreateMap<MinimalUserInfo, UserResponse>().ReverseMap();

            CreateMap<UsingDetail, UsingDetailResponse>()
                .ForMember(des=>des.UseValue, o=>o.MapFrom(s=>$"{s.UseValue} ({s.Unit})"))
                .ReverseMap();
            CreateMap<TreatmentDetail, TreatmentDetailResponse>().ReverseMap();
            CreateMap<BaseComponent, ComponentResponse>()
                .ForMember(des=>des.Type, o=>o.MapFrom(s=>s.GetStringType()))
                .ReverseMap();

        }
    }
}
