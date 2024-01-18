using AutoMapper;
using EventBus.Events;
using Service.FarmRegistry.Commands;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;

namespace Service.Registration.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile() {
            CreateMap<PackageSolution, SolutionResponse>().ReverseMap();
            CreateMap<RegisterFormResponse, FarmRegistration>().ReverseMap();

            CreateMap<RegistFarmCommand, FarmRegistration>().ReverseMap();
            CreateMap<RegisterFormResponse, FarmRegistration>().ReverseMap();

            CreateMap<AcceptFarmRegistEvent, FarmRegistration>().ReverseMap();

            
        }
    }
}
