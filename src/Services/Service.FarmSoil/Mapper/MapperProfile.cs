using AutoMapper;
using Service.Soil.Command;
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

            CreateMap<LandRequest, FarmSoil>().ReverseMap();

            CreateMap<FarmSoil, LandWithPropertiesResponse>()
                .ForMember(dest=>dest.SiteName, opt => opt.MapFrom(src=>src.Site.Name))
                .ReverseMap();

            CreateMap<ComponentProperty, PropertyResponse>().ReverseMap();

        }
    }
}
