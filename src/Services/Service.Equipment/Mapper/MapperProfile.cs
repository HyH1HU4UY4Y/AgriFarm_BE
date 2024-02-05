using AutoMapper;
using SharedDomain.Entities.FarmComponents;
using Service.Equipment.DTOs;

namespace Service.Equipment.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EquipmentRequest, FarmEquipment>().ReverseMap();
            CreateMap<FarmEquipment, EquipmentResponse>().ReverseMap();



        }
    }
}
