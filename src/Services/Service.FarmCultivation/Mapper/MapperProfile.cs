using AutoMapper;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;

namespace Service.FarmCultivation.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            CreateMap<SeasonRequest, CultivationSeason>().ReverseMap();
            CreateMap<CultivationSeason, SeasonResponse>().ReverseMap();
            CreateMap<CultivationSeason, SeasonDetailResponse>().ReverseMap();
            

            CreateMap<HarvestProductRequest, HarvestProduct>().ReverseMap();
            CreateMap<HarvestProduct, HarvestProductResponse>().ReverseMap();
            

        }
    }
}
