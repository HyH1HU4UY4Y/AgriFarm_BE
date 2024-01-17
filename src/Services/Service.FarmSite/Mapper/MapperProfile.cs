using AutoMapper;
using Service.FarmSite.Commands;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;

namespace Service.FarmSite.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateNewFarmCommand, Site>().ReverseMap();
            CreateMap<CreateSubscriptBillCommand, Subscripton>().ReverseMap();
            CreateMap<AddCapitalStateCommand, CapitalState>().ReverseMap();
        }
    }
}
