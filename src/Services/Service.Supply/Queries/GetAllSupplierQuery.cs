using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.Commands;
using Service.Supply.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries
{
    public class GetAllSupplierQuery: IRequest<List<SupplierResponse>>
    {
    }

    public class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, List<SupplierResponse>>
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

        public async Task<List<SupplierResponse>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var rs = await _suppliers.GetMany();

            return _mapper.Map<List<SupplierResponse>>(rs);
        }
    }
}
