using AutoMapper;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;

namespace Service.Seed.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            CreateMap<PropertyValue, ComponentProperty>().ReverseMap();

            CreateMap<SeedCreateRequest, FarmSeed>().ReverseMap();
            CreateMap<SeedInfoRequest, FarmSeed>().ReverseMap();
            CreateMap<FarmSeed, SeedResponse>().ReverseMap();

            CreateMap<RefSeedRequest, ReferencedSeed>().ReverseMap();
            CreateMap<ReferencedSeed, RefSeedResponse>().ReverseMap();


        }
    }
}
