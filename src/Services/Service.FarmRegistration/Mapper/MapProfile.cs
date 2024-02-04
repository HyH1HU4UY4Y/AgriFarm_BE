using AutoMapper;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;

namespace Service.Registration.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile() {
            CreateMap<PackageSolution, SolutionResponse>().ReverseMap();
            CreateMap<RegisterFormResponse, FarmRegistration>().ReverseMap();
        }
    }
}
