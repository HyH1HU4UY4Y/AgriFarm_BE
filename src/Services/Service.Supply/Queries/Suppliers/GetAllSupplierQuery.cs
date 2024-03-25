using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.Commands;
using Service.Supply.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries.Suppliers
{
    public class GetAllSupplierQuery : IRequest<PagedList<SupplierInfoResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId { get; set; }
    }

    public class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, PagedList<SupplierInfoResponse>>
    {

        private ISQLRepository<SupplyContext, Supplier> _suppliers;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<GetAllSupplierQueryHandler> _logger;
        private IMapper _mapper;

        public GetAllSupplierQueryHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<GetAllSupplierQueryHandler> logger,
            IMapper mapper)
        {
            _suppliers = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedList<SupplierInfoResponse>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var rs = await _suppliers.GetMany(e=>e.CreatedByFarmId == request.SiteId);

            return PagedList<SupplierInfoResponse>.ToPagedList(
                    _mapper.Map<List<SupplierInfoResponse>>(rs),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
