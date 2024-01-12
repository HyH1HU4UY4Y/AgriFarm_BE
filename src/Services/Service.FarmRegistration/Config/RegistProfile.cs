using AutoMapper;
using Service.FarmRegistry.Commands;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;

namespace Service.FarmRegistry.Config
{
    public class RegistProfile: Profile
    {
        public RegistProfile() { 
            CreateMap<RegistFarmCommand, FarmRegistration>().ReverseMap();
            CreateMap<RegisterFormResponse, FarmRegistration>().ReverseMap();
            CreateMap<PackageSolution, SolutionResponse>().ReverseMap();
        }
    }
}
