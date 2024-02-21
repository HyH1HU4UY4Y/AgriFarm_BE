using AutoMapper;
using Infrastructure.Equipment.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Equipment.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Queries
{
    public class GetEquipmentsQuery: IRequest<PagedList<EquipmentResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId { get; set; }

    }

    public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, PagedList<EquipmentResponse>>
    {

        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<GetEquipmentsQueryHandler> _logger;

        public GetEquipmentsQueryHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<GetEquipmentsQueryHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<EquipmentResponse>> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
        {
            var items = await _equipments.GetMany(e => e.SiteId == request.SiteId,
                                                ls => ls.Include(x => x.Properties));



            return PagedList<EquipmentResponse>.ToPagedList(
                    _mapper.Map<List<EquipmentResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }


}
