using AutoMapper;
using Service.Soil.DTOs;
using SharedDomain.Entities.FarmComponents;

namespace Service.Soil.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            CreateMap<FarmSoil, LandResponse>()
                .ForMember(dest=>dest.SiteName, opt => opt.MapFrom(src=>src.Site.Name))
                .ReverseMap();
        }
    }
}
