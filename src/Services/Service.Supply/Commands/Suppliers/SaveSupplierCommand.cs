using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Suppliers
{
    public class SaveSupplierCommand : IRequest<SupplierInfoResponse>
    {
        public SupplierRequest Supplier {  get; set; }
        public Guid SiteId { get; set; }
    }

    public class SaveSupplierCommandHandler : IRequestHandler<SaveSupplierCommand, SupplierInfoResponse>
    {
        private ISQLRepository<SupplyContext, Supplier> _supplies;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<SaveSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public SaveSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<SaveSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _supplies = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SupplierInfoResponse> Handle(SaveSupplierCommand request, CancellationToken cancellationToken)
        {

            var item = await _supplies
                .GetOne(e => 
                        (string.Equals(e.Name.Trim().ToUpper(), request.Supplier.Name.Trim().ToUpper()) 
                        || e.Address!.Equals(request.Supplier.Address!))
                        && e.CreatedByFarmId == request.SiteId
                        );

            if (item == null)
            {
                item = _mapper.Map<Supplier>(request.Supplier);
                item.CreatedByFarmId = request.SiteId;
                item = await _supplies.AddAsync(item);
                await _unit.SaveChangesAsync(cancellationToken);
            }

            return _mapper.Map<SupplierInfoResponse>(item);
        }
    }


}
