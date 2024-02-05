using MediatR;
using Service.Equipment.DTOs;
using SharedApplication.Pagination;

namespace Service.Equipment.Queries
{
    public class GetEquipmentByIdQuery : IRequest<EquipmentResponse>
    {
        public Guid Id { get; set; }

    }
}
