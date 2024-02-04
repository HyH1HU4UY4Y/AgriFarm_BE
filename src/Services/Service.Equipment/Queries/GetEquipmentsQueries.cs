using MediatR;
using Service.Equipment.DTOs;
using SharedApplication.Pagination;

namespace Service.Equipment.Queries
{
    public class GetEquipmentsQuery: IRequest<PagedList<EquipmentResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();

    }

    
}
