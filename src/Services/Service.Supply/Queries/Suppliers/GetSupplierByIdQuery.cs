using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries.Suppliers
{
    public class GetSupplierByIdQuery : IRequest<SupplierInfoResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierInfoResponse>
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

        public async Task<SupplierInfoResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _suppliers.GetOne(e => e.Id == request.Id);

            if (rs == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<SupplierInfoResponse>(rs);
        }
    }
}
