using AutoMapper;
using Service.Supply.Commands.Suppliers;
using Service.Supply.Commands.Supplies;
using Service.Supply.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;

namespace Service.Supply.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Supplier, SupplierResponse>().ReverseMap();
            CreateMap<SupplierRequest, Supplier>().ReverseMap();

            CreateMap<SupplyDetail, SupplyDetailResponse>()
                //.ForMember(des=>des.Amount, o=>o.MapFrom(s=> $"{s.Quantity} ({s.Unit})"))
                .ForMember(des=>des.ExpiredIn,
                            o=>o.MapFrom(s=> (s.ExpiredIn == null)?
                            "None": ((DateTime)s.ExpiredIn).ToString("yyyy/MM/dd")))
                .ReverseMap();

            CreateMap<AddNewContractCommand, SupplyDetail>().ReverseMap();

            CreateMap<NewContractRequest, AddNewContractCommand>().ReverseMap();

            CreateMap<BaseComponent, SupplyItemResponse>().ReverseMap();

        }
    }
}
