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

            CreateMap<FertilizeRequest, FarmFertilize>().ReverseMap();
            CreateMap<FarmFertilize, FertilizeResponse>().ReverseMap();

            CreateMap<RefFertilizeRequest, ReferencedFertilize>().ReverseMap();
            CreateMap<ReferencedFertilize, RefFertilizeResponse>().ReverseMap();
        }
    }
}
