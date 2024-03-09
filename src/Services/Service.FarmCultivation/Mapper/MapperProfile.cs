using AutoMapper;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.DTOs.Products;
using Service.FarmCultivation.DTOs.Seasons;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;

namespace Service.FarmCultivation.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
            CreateMap<SeasonCreateRequest, CultivationSeason>().ReverseMap();
            CreateMap<SeasonEditRequest, CultivationSeason>().ReverseMap();
            CreateMap<CultivationSeason, SeasonResponse>().ReverseMap();
            CreateMap<CultivationSeason, SeasonDetailResponse>().ReverseMap();

            CreateMap<SeedVM, FarmSeed>().ReverseMap();
            CreateMap<LandVM, FarmSoil>().ReverseMap();
            

            CreateMap<ProductRequest, HarvestProduct>().ReverseMap();
            CreateMap<HarvestProduct, ProductResponse>().ReverseMap();
            

        }
    }
}
