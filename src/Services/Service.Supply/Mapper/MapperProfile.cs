using AutoMapper;
using Service.Supply.Commands;
using Service.Supply.DTOs;

namespace Service.Supply.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SupplierRequest, CreateNewSupplierCommand>().ReverseMap();
            CreateMap<SupplierRequest, CreateNewSupplierCommand>().ReverseMap();
        }
    }
}
