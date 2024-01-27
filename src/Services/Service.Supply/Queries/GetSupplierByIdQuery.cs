using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries
{
    public class GetSupplierByIdQuery: IRequest<SupplierDetailResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDetailResponse>
    {

        private ISQLRepository<SupplyContext, Supplier> _suppliers;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<GetSupplierByIdQueryHandler> _logger;
        private IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<GetSupplierByIdQueryHandler> logger,
            IMapper mapper)
        {
            _suppliers = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SupplierDetailResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _suppliers.GetOne(e=>e.Id == request.Id);

            return _mapper.Map<SupplierDetailResponse>(rs);
        }
    }
}
