using AutoMapper;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Entities.FarmComponents;
using Service.Fertilize.DTOs;

namespace Service.Fertilize.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PropertyValue, ComponentProperty>().ReverseMap();

            CreateMap<FertilizeCreateRequest, FarmFertilize>().ReverseMap();
            CreateMap<FertilizeInfoRequest, FarmFertilize>().ReverseMap();
            CreateMap<FarmFertilize, FertilizeResponse>().ReverseMap();

            CreateMap<RefFertilizeRequest, ReferencedFertilize>().ReverseMap();
            CreateMap<ReferencedFertilize, RefFertilizeResponse>().ReverseMap();
        }
    }
}
