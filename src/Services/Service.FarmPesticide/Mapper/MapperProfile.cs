using AutoMapper;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;

namespace Service.Pesticide.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {
            CreateMap<PropertyValue, ComponentProperty>().ReverseMap();

            CreateMap<PesticideCreateRequest, FarmPesticide>().ReverseMap();
            CreateMap<PesticideInfoRequest, FarmPesticide>().ReverseMap();
            CreateMap<FarmPesticide, PesticideResponse>().ReverseMap();

            CreateMap<RefPesticideRequest, ReferencedPesticide>().ReverseMap();
            CreateMap<ReferencedPesticide, RefPesticideResponse>().ReverseMap();
        }
    }
}
