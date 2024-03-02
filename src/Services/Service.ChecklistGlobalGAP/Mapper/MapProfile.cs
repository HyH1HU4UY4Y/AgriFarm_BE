using AutoMapper;
using Service.ChecklistGlobalGAP.DTOs;
using SharedDomain.Entities.ChecklistGlobalGAP;

namespace Service.ChecklistGlobalGAP.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ChecklistMapping, ChecklistMappingDTO>().ReverseMap();
            CreateMap<ChecklistMaster, ChecklistMasterDTO>().ReverseMap();
            CreateMap<ChecklistItemResponse, ChecklistItemResponseDTO>().ReverseMap();
        }
    }
}
