using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.Commands;
using Service.Supply.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries.Suppliers
{
    public class GetAllSupplierQuery : IRequest<PagedList<SupplierResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, PagedList<SupplierResponse>>
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

        public async Task<PagedList<SupplierResponse>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var rs = await _suppliers.GetMany();

            return PagedList<SupplierResponse>.ToPagedList(
                    _mapper.Map<List<SupplierResponse>>(rs),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
