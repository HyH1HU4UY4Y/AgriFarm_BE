using AutoMapper;
using Service.Water.DTOs;
using SharedDomain.Entities.FarmComponents;

namespace Service.Water.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            CreateMap<WaterRequest, FarmWater>().ReverseMap();
            CreateMap<FarmWater, WaterResponse>().ReverseMap();
        }
    }
}
