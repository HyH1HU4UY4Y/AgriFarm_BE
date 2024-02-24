using AutoMapper;
using Service.FarmSite.Commands;
using Service.FarmSite.Commands.Farms;
using Service.FarmSite.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Others;
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

            CreateMap<SiteEditRequest, Site>().ReverseMap();
            CreateMap<SiteCreateRequest, Site>().ReverseMap();

            CreateMap<Site, SiteResponse>().ReverseMap();
            CreateMap<Site, FullSiteResponse>().ReverseMap();
        }
    }
}
